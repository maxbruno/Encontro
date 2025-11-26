using Microsoft.EntityFrameworkCore;

namespace Encontro.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Models.Pessoa> Pessoas { get; set; }
    }
}
