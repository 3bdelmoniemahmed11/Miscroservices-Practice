using platformService.Controllers.Platforms.DTOs;

namespace PaltformService.SyncDataServices.Http
{
    public interface ICommandDataClient
    {

        public Task SendPlatformService(PlatformReadDto platformService);
    }
}
