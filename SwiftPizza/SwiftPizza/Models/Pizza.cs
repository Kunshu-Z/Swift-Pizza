/// <summary>
/// Pizza class contains the primary key, and attributes which will be utilised within the Pizza database table.
/// The Pizza table will contain all details regarding the pizza, i.e., Name, Price, and Description. Quantity of Pizza will be 
/// Determined by the user, and PizzaID is only for reference when entering new pizza entries to the Pizza table.
/// </summary>

using System.ComponentModel.DataAnnotations;

namespace SwiftPizza.Models
{
    public class Pizza
    {
        //Primary Key
        [Key]
        public int PizzaId { get; set; }

        //Attributes
        [Required]

        public string PizzaName { get; set; }

        public int PizzaPrice { get; set; }
        
        public int PizzaQuantity { get; set; }

        public string PizzaDescription { get; set; }

        public string PizzaImage { get; set; }
    }
}
