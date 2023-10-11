/// <summary>
/// Bank class contains the primary key, foreign keys, and attributes which will be utilised within the Bank database table.
/// This will be an advanced feature on the web app where the user can make a purchase through inputting card details.
/// </summary>

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SwiftPizza.Models
{
    public class Bank
    {
        [Key]
        public int BankId { get; set; } // Unique identifier for each record

        [Required]
        [StringLength(19, MinimumLength = 12)] // 12 to 19 characters to cover different card lengths
        public string CardNumber { get; set; }

        [Required]
        public DateTime Expiry { get; set; }

        [Required]
        [Range(100, 999)] // CVV is typically a 3-digit code
        public int CVV { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual User User { get; set; }

        [ForeignKey("Cart")]
        public int CartId { get; set; }
        public virtual Cart Cart { get; set; }
    }

}
