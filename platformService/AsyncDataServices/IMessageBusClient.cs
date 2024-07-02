using PaltformService.Controllers.Platforms.DTOs;

namespace PaltformService.AsyncDataServices
{
    public interface IMessageBusClient
    {
        void PublishNewPlatform(PlatformPublishDto platformPublish);
    }
}
