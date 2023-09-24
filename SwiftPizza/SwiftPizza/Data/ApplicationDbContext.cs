using Microsoft.EntityFrameworkCore;
using SwiftPizza.Models;

namespace SwiftPizza.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // Define DbSet properties for your tables
        public DbSet<User> Users { get; set; }
        public DbSet<Pizza> Pizzas { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Bank> Banks { get; set; }
    }
}
