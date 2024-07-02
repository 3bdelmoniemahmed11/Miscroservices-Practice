using CommandsService.Context;
using CommandsService.IRepositories;
using CommandsService.Models;
using Microsoft.EntityFrameworkCore;

namespace CommandsService.Repositories
{
    public class CommandRepository : ICommandRepository
    {
        private readonly AppDbContext _context;

        public CommandRepository(AppDbContext context)
        {
            _context = context; 
        }
        public async Task CreateCommandAsync(Command command)
        {
           await _context.Commands.AddAsync(command);   
            await SaveChangesAsync();
        }

        public async Task CreatePlatformAsync(Platform platform)
        {
           await _context.Platform.AddAsync(platform);  
            await SaveChangesAsync();   
        }

        public  bool ExternalPlatformExsits(Guid externalPlatformId)
        {
            return  _context.Platform.Any(p => p.ExternalId == externalPlatformId);
        }

        public async Task<List<Platform>> GetAllPlatformsAsync()
        {
          return    await _context.Platform.ToListAsync();
        }

        public async Task<Command> GetCommandAsync(Guid platformId, Guid commandId)
        {
          return  await _context.Commands.Where(c=>c.PlatformId==platformId && c.Id==commandId).FirstOrDefaultAsync(); 
        }

        public async Task<List<Command>> GetCommandsForPlatformAsync(Guid platformId)
        {
           return await _context.Commands.Where(c => c.PlatformId == platformId).ToListAsync();
        }

        public async Task<Platform> GetPlatformByIdAsync(Guid platformId)
        {
           return await _context.Platform.Where(p=>p.Id==platformId).FirstOrDefaultAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
