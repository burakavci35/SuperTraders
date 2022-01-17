using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTraders.DataService.Models
{
    public class TransactionLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption
            .Identity)]
        public int Id { get; set; }
        public int AccountId { get; set; }

        public string Type { get; set; }

        public decimal Price { get; set; }

        public decimal Amount { get; set; } 

        public string Symbol { get; set; }

        public Account Account { get; set; }
    }
}
