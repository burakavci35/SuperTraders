using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuperTraders.DataService.Models
{
    public class Share
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption
            .Identity)]
        public int Id { get; set; }

        [RegularExpression(@"[A-Z]{3}",
            ErrorMessage = "should be all capital letters with 3 characters.")]
        public string Symbol { get; set; }

        [RegularExpression(@"^[0-9]*\.[0-9]{2}$",
            ErrorMessage = "should be exactly 2 decimal digits.")]
        public decimal Rate { get; set; }

        public DateTime LastUpDateTime { get; set; }
    }
}
