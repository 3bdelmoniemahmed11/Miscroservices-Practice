using Microsoft.EntityFrameworkCore;
using PlatformService.Context;
using PlatformService.IReposatiory;

namespace PlatformService.Repositories
{
    public class PlatformRepository(AppDbContext _context) : IPlatformRepository
    {
        public async Task CreateAsync(Platform platform) => await _context.Platform.AddAsync(platform);
       
        public async Task<List<Platform>> GetAllAsync() => await _context.Platform.ToListAsync();

        public async Task<Platform> GetByIdAsync(Guid id) => await _context.Platform.FirstOrDefaultAsync(p => p.Id == id);

        public Task SaveChangesAsync() => _context.SaveChangesAsync();
    
    }
}
