using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

		//Foreign Key (UserID)
		[ForeignKey("User")]
		public int UserID { get; set; }
		//Navigation property
		public User User { get; set; }

		//Foreign Key (CartID)
		[ForeignKey("Cart")]
		public int CartID { get; set; }
		//Navigation property
		public Cart Cart { get; set; }
	}
}
