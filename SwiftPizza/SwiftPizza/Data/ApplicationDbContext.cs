using Microsoft.EntityFrameworkCore;
using SwiftPizza.Models;

namespace SwiftPizza.Data
{
    public interface IApplicationDbContext
    {
        DbSet<Pizza> Pizza { get; set; }
    }
        public class ApplicationDbContext : DbContext, IApplicationDbContext
        { 
            public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
            {
            }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
            modelBuilder.Entity<User>().ToTable("User");
            // any other model configurations can go here
            modelBuilder.Entity<Pizza>().ToTable("Pizza");
        }

        // Define DbSet properties for your tables
        public DbSet<User> Users { get; set; }
        public DbSet<Pizza> Pizza { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Bank> Bank { get; set; }
    }
}
