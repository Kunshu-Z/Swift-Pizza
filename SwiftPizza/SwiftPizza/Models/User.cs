using System.ComponentModel.DataAnnotations;
namespace SwiftPizza.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }

        [Required]

        public string Username { get; set; }

        public string Password {  get; set; }

        public bool isAdmin { get; set; }

    }
}
