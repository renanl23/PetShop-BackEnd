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
    }
}