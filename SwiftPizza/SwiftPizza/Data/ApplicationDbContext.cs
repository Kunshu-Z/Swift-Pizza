using Microsoft.EntityFrameworkCore;
using SwiftPizza.Models;

namespace SwiftPizza.Data
{
    public class ApplicationDbContext : DbContext
    {
        // Parameterless constructor for Moq
        public ApplicationDbContext() { }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");
            // any other model configurations can go here
            modelBuilder.Entity<Pizza>().ToTable("Pizza");
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Pizza> Pizzas { get; set; }
        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<Bank> Bank { get; set; }
    }
}
