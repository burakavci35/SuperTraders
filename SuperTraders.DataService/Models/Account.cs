using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTraders.DataService.Models
{
    public class Account
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public decimal TotalBalance { get; set; }

        public List<AccountShare> AccountShareList { get; } = new();

        public List<TransactionLog> TransactionLogs { get; } = new();   

        public bool CheckAccountBalanceForBuy(decimal totalShare)
        {
            return TotalBalance >= totalShare;
        }

        public bool CheckAccountShareAvailableForSell(string symbol,decimal amount)
        {
           var foundAccount = AccountShareList.FirstOrDefault(x => x.Symbol == symbol);

           return foundAccount?.Amount >= amount;
        }
    }
}
