using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace PetShop_BackEnd.Models
{
    public class LogContext : DbContext
    {
        public LogContext(DbContextOptions<LogContext> options)
            : base(options)
        {
        }

        public DbSet<Log> Logs { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Log>().HasKey(m => m.id);
            base.OnModelCreating(builder);
        }
    }
}