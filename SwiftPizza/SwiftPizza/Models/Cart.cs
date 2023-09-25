using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;

namespace SwiftPizza.Models
{
    public class Cart
    {
        [Key]
        public int CartId { get; set; } 

        [Required]
        public int Price { get; set; }
    }
}
