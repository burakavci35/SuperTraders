using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTraders.DataService.Models
{
    public class Trade
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Symbol { get; set; }
        [Required]
        [Range(0.01, 999999999, ErrorMessage = "Price must be greater than 0.00")]
        //[RegularExpression(@"^\d+\.\d{0,2}$",
        //    ErrorMessage = "should be exactly 2 decimal digits.")]
        public decimal Amount { get; set; }
    }
}
