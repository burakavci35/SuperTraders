using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTraders.DataService.Models
{
    public class AccountShare
    {
        public int Id { get; set; }

        public string Symbol { get; set; }

        public decimal Amount { get; set; }

        public int AccountId { get; set; }
        public Account Account { get; set; }

    }
}
