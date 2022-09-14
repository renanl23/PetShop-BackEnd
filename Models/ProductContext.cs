using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace PetShop_BackEnd.Models
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Product>().HasKey(m => m.id);
            base.OnModelCreating(builder);
        }
    }
}