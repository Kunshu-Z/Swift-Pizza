using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SwiftPizza.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }

        [Required]

        public string Username { get; set; }

        public string Password {  get; set; }

		//Foreign Key (CardNumber)
		[ForeignKey("Bank")]
		public int CardNumber { get; set; }
		//Navigation Property
		public Bank Bank { get; set; }
	}
}
