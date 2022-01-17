using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SuperTraders.DataService.Interface;
using SuperTraders.DataService.Models;

namespace SuperTraders.DataService.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly SuperTradersContext _context;

        public AccountRepository(SuperTradersContext context)
        {
            _context = context;
        }


        public bool IsAccountExistByUserName(string userName)
        {
           return _context.Accounts.Any(x=>x.UserName == userName);
        }

        public Task<Account> GetAccountByUserName(string userName)
        {
            return _context.Accounts.Include(x=>x.AccountShareList).FirstOrDefaultAsync(x => x.UserName == userName);
        }


        public async Task AddAccountShare(string userName, AccountShare accountShare, decimal totalShareOfPrice)
        {
            var foundAccount = await GetAccountByUserName(userName);

            var foundShare = foundAccount.AccountShareList.FirstOrDefault(x => x.Symbol == accountShare.Symbol);

            if (foundShare == null)
            {
                foundAccount.AccountShareList.Add(accountShare);
            }
            else
            {
                foundShare.Amount += accountShare.Amount;
            }

            foundAccount.TotalBalance -= totalShareOfPrice;

            await _context.SaveChangesAsync();

        }

        public async Task RemoveAccountShare(string userName, AccountShare accountShare, decimal totalShareOfPrice)
        {
            var foundAccount = await GetAccountByUserName(userName);

            var foundShare = foundAccount.AccountShareList.FirstOrDefault(x => x.Symbol == accountShare.Symbol);

            if (foundShare?.Amount == accountShare.Amount)
            {
                foundAccount.AccountShareList.Remove(accountShare);
            }
            else
            {
                foundShare.Amount -= accountShare.Amount;
            }

            foundAccount.TotalBalance += totalShareOfPrice;

            await _context.SaveChangesAsync();
        }
    }
}
