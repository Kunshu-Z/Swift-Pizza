/// <summary>
/// Cart class contains the primary key, foreign keys, and attributes which will be utilised within the Cart database table.
/// The purpose of this class is to serve as a container for the user's selected pizza items.
/// The CartID used as a foreign key for the bank class to link the transaction with the items the user has selected.
/// </summary>

using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SwiftPizza.Models
{
    public class Cart
    {
        //Primary Key
        [Key]
        public int CartId { get; set; }

        //Attributes
        [Required]
        public int Price { get; set; }

		//Foreign Key (PizzaID)
		[ForeignKey("Pizza")]
        public int PizzaID { get; set; }
        //Navigation Property
        public Pizza Pizza { get; set; }
    }
}
