using System.ComponentModel.DataAnnotations;

namespace SwiftPizza.Models
{
    public class Bank
    {
        [Key]
        public int CardNumber { get; set; }
        [Required]
        public string Name { get; set; }

        public DateTime Expiry { get; set; }

        public int CVV { get; set; }
    }
}
