using platformService.Controllers.Platforms.DTOs;
using System.Text.Json;
using System.Text.Unicode;

namespace PaltformService.SyncDataServices.Http
{
    public class CommandDataClient : ICommandDataClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        public CommandDataClient(HttpClient httpClient ,IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration=configuration;
        }
        public async Task SendPlatformService(PlatformReadDto platformService)
        {
            var httpContent = new StringContent(
                JsonSerializer.Serialize(platformService),
                System.Text.Encoding.UTF8,
                "application/json");
            var response = await _httpClient.PostAsync($"{_configuration["CommandServiceUrl"]}api/Platforms", httpContent);
            if (response is not null) Console.WriteLine("sync to command service is created");
            else  Console.WriteLine("sync to command Service is not created");
        }
    }
}
