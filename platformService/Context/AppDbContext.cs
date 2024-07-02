using Microsoft.EntityFrameworkCore;

namespace PlatformService.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {

        }
        public DbSet<Platform> Platform { get; set; }
    }
}
