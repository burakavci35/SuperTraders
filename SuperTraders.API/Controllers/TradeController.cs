using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SuperTraders.DataService.Interface;
using SuperTraders.DataService.Models;

namespace SuperTraders.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TradeController : ControllerBase
    {
        private readonly IShareRepository _shareRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly ITransactionLogRepository _transactionLogRepository;

        public TradeController(IShareRepository shareRepository, IAccountRepository accountRepository,
            ITransactionLogRepository transactionLogRepository)
        {
            _shareRepository = shareRepository;
            _accountRepository = accountRepository;
            _transactionLogRepository = transactionLogRepository;
        }

        [HttpPost("BUY")]
        public async Task<IActionResult> Buy(Trade trade)
        {
            try
            {
                var shareList = await _shareRepository.GetShareList();

                if (!shareList.Exists(x => x.Symbol == trade.Symbol))
                    return BadRequest($"Requested Symbol : {trade.Symbol} Not Found in ShareList");

                if (!_accountRepository.IsAccountExistByUserName(trade.UserName))
                    return BadRequest($"Requested UserName : {trade.UserName} Not found!");

                var totalOfShares = await _shareRepository.CalculateTotalOfShares(trade.Symbol, trade.Amount);

                var foundAccount = await _accountRepository.GetAccountByUserName(trade.UserName);

                var accountBalanceIsAvailable = foundAccount.CheckAccountBalanceForBuy(totalOfShares);

                if (!accountBalanceIsAvailable)
                    return BadRequest($"not have enough balance");

                // Insert Account Share
                await _accountRepository.AddAccountShare(foundAccount.UserName,
                    new AccountShare() {Symbol = trade.Symbol, Amount = trade.Amount}, totalOfShares);

                await _transactionLogRepository.AddLogByUserName(trade.UserName,
                    new TransactionLog()
                    {
                        Symbol = trade.Symbol, Amount = trade.Amount, Type = "BUY", Price = totalOfShares
                    });

                return Ok(trade);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost("SELL")]
        public async Task<IActionResult> Sell(Trade trade)
        {
            try
            {
                var shareList = await _shareRepository.GetShareList();

                if (!shareList.Exists(x => x.Symbol == trade.Symbol))
                    return BadRequest($"Requested Symbol : {trade.Symbol} Not Found in ShareList");

                if (!_accountRepository.IsAccountExistByUserName(trade.UserName))
                    return BadRequest($"Requested UserName : {trade.UserName} Not found!");

                var totalSharesOfPrice = await _shareRepository.CalculateTotalOfShares(trade.Symbol, trade.Amount);

                var foundAccount = await _accountRepository.GetAccountByUserName(trade.UserName);

                var accountShareIsAvailable =
                    foundAccount.CheckAccountShareAvailableForSell(trade.Symbol, trade.Amount);

                if (!accountShareIsAvailable)
                    return BadRequest($"not have enough Share");

                //Remove Account Share
                await _accountRepository.RemoveAccountShare(trade.UserName,
                    new AccountShare() {Symbol = trade.Symbol, Amount = trade.Amount}, totalSharesOfPrice);

                await _transactionLogRepository.AddLogByUserName(trade.UserName,
                    new TransactionLog()
                    {
                        Symbol = trade.Symbol,
                        Amount = trade.Amount,
                        Type = "SELL",
                        Price = totalSharesOfPrice,

                    });

                return Ok(trade);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}