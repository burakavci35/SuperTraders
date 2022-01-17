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
    public class TransactionLogRepository : ITransactionLogRepository
    {
        private readonly SuperTradersContext _context;

        public TransactionLogRepository(SuperTradersContext context)
        {
            _context = context;
        }

        public async Task AddLog(TransactionLog transactionLog)
        {
            _context.TransactionLogs.Add(transactionLog);
            await _context.SaveChangesAsync();
        }

        public async Task AddLogByUserName(string userName, TransactionLog transactionLog)
        {
            var foundAccount = await _context.Accounts.FirstOrDefaultAsync(x => x.UserName == userName);

            foundAccount.TransactionLogs.Add(transactionLog);

            await _context.SaveChangesAsync();
        }

        public Task<List<TransactionLog>> GetLogsByUserName(string userName)
        {
            return _context.TransactionLogs.Include(x=>x.Account).Where(x=>x.Account.UserName == userName).ToListAsync();
        }
    }
}
