using System.ComponentModel.DataAnnotations;

namespace SwiftPizza.Models
{
    public class Bank
    {
        public int CardNumber { get; set; }
        
        public string Address { get; set; }

        public DateTime Expiry { get; set; }

        public int CVV { get; set; }
    }
}
