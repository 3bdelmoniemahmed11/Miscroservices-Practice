namespace PlatformService.IReposatiory
{
    public interface IPlatformRepository
    {

        public Task  SaveChangesAsync();
        public Task<List<Platform>> GetAllAsync();
        public Task<Platform> GetByIdAsync(Guid id);
        public Task  CreateAsync(Platform platform);    

    }
}
