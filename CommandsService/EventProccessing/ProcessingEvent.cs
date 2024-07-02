using AutoMapper;
using CommandsService.Common;
using CommandsService.Controllers.DTOs;
using CommandsService.IRepositories;
using CommandsService.Models;
using RabbitMQ.Client.Exceptions;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text.Json;

namespace CommandsService.EventProccessing
{
    public class ProcessingEvent : IProcessingEvent
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IMapper _mapper;

        public ProcessingEvent(IServiceScopeFactory scopeFactory,    IMapper mapper)
        {
            _scopeFactory = scopeFactory;
            _mapper = mapper;   
        }
        public void ProcessEvent(string message)
        {
           var eventType=DetermineEventType(message);   
            if(eventType==EventType.PlatformPublished)
            {
                addPlatform(message);
            }else
            {
                Console.WriteLine("unknown event type");
            }
        }


        private EventType DetermineEventType(string message )
        {
            var eventtype = JsonSerializer.Deserialize<GenericEvent>(message);
            switch(eventtype.Event)
            {
                case "Platform_Published" :
                    Console.WriteLine("The  event is Platform_Published ");
                    return EventType.PlatformPublished;
                default:
                    Console.WriteLine("The  event is undetermined");
                    return EventType.Undetermined;
            }
        }

        private  void addPlatform(string platformPublishedMessage)
        {
               using( var scope = _scopeFactory.CreateScope())
            {
                var repo = scope.ServiceProvider.GetRequiredService<ICommandRepository>();
                var platformPublishDto=JsonSerializer.Deserialize<PlatformPublishDto>(platformPublishedMessage);    
               
                try
                {
                    var platform = _mapper.Map<Platform>(platformPublishDto);
                    if (!repo.ExternalPlatformExsits(platform.ExternalId))
                    {
                        repo.CreatePlatformAsync(platform);
                        repo.SaveChangesAsync();
                    }else
                    {

                        Console.WriteLine($"this platform {platform.ExternalId} is added before");
                    }
                   

                }catch(Exception ex)
                {

                    Console.WriteLine(ex.Message);   
                }
            }
        }
            
    }

    enum EventType
    {

        PlatformPublished,
        Undetermined
    }
}
