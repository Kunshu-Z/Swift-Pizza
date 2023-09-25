using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SwiftPizza.Models
{
    public class Cart
    {
        [Key]
        public int CartId { get; set; }

        [Required]
        public int Price { get; set; }

		//Foreign Key (PizzaID)
		[ForeignKey("Pizza")]
        public int PizzaID { get; set; }
        //Navigation Property
        public Pizza Pizza { get; set; }
    }
}
