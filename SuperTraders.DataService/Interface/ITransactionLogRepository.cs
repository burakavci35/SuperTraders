using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperTraders.DataService.Models;

namespace SuperTraders.DataService.Interface
{
    public interface ITransactionLogRepository
    {
        Task AddLogByUserName(string userName,TransactionLog transactionLog);

        Task<List<TransactionLog>> GetLogsByUserName(string userName);
    }
}
