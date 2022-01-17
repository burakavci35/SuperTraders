using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperTraders.DataService.Models;

namespace SuperTraders.DataService.Interface
{
    public interface IAccountRepository
    {
        bool IsAccountExistByUserName(string userName);

        Task<Account> GetAccountByUserName(string userName);

        Task AddAccountShare(string userName, AccountShare accountShare, decimal totalShareOfPrice); 

        Task RemoveAccountShare(string userName, AccountShare accountShare,decimal totalShareOfPrice);
    }
}
