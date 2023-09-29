/// <summary>
/// User class contains the primary key, foreign keys, and attributes which will be utilised within the User database table.
/// This class will contain basic details concerning the user, and the UserID will be utilised as a foreign key for the Bank table
/// To validate the user before making a purchase.
/// </summary>

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SwiftPizza.Models
{
    public class User
    {
        //Primary Key
        [Key]
        public int UserID { get; set; }

        //Attributes
        [Required]
        public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
        public string Password {  get; set; }
		public string Address { get; set; }
		public int PhoneNumber { get; set; }
	}
}
