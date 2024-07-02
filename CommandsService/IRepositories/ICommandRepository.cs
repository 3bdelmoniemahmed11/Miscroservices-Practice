using CommandsService.Models;

namespace CommandsService.IRepositories
{
    public interface ICommandRepository
    {

        public Task SaveChangesAsync();
        public Task<List<Platform>> GetAllPlatformsAsync();
        public Task CreatePlatformAsync(Platform platform);
        public Task<Platform> GetPlatformByIdAsync(Guid platformId);    


        public Task CreateCommandAsync(Command command);
        public Task<List<Command>> GetCommandsForPlatformAsync(Guid platformId);
        public Task<Command> GetCommandAsync(Guid platformId, Guid commandId);
        public bool ExternalPlatformExsits(Guid externalPlatformId);
    }
}
