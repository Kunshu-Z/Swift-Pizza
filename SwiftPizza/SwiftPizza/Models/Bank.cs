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
		//Primary Key
		[Key]
        public int CardNumber { get; set; } 

		//Attributes
        [Required]
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
