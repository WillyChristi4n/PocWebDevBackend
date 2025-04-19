using Microsoft.EntityFrameworkCore;

namespace PocWebDevBackend.Models
{
    public class AppDBContext: DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }

        public DbSet<Veichle> Veichles { get; set; }

        public DbSet<Consumption> Consumptions { get; set; }

        public DbSet<User> Users { get; set; }
    }
}
