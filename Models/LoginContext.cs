using Microsoft.EntityFrameworkCore;
namespace PetShop_BackEnd.Models
{
    public class LoginContext : DbContext
    {
        public LoginContext(DbContextOptions<LoginContext> options)
            : base(options)
        {
        }

        public DbSet<LoginItem> LoginItems { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<LoginItem>().HasKey(m => m.Id);
            base.OnModelCreating(builder);
        }
    }
}