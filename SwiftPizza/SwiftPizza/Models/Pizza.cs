using System.ComponentModel.DataAnnotations;

namespace SwiftPizza.Models
{
    public class Pizza
    {
        [Key]

        public int PizzaId { get; set; }
        [Required]

        public string PizzaName { get; set; }

        public int PizzaPrice { get; set; }
        
        public int PizzaQuantity { get; set; }

        public string PizzaDescription { get; set; }
    }
}
