using System.Text.Json;
using AutoMapper;
using CommandService.Data;
using CommandService.Dtos;
using CommandService.Models;
using Common.Dtos;
using Common.Events;

namespace CommandService.EventProcessing
{
    public class EventProcessor : IEventProcessor
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IMapper _mapper;

        public EventProcessor(IServiceScopeFactory scopeFactory, IMapper mapper)
        {
            _scopeFactory = scopeFactory;
            _mapper = mapper;
        }

        public void ProcessEvent(string message)
        {
            var eventType = DetermineEventType(message);

            switch(eventType)
            {
                case EventType.PlatformPublish:
                    AddPlatform(message);
                    break;
                default:
                    Console.WriteLine($"Event type not determined");
                    break;
            }
        }

        private void AddPlatform(string platformPublishMessage)
        {
            using(var scope = _scopeFactory.CreateScope())
            {
                var repo = scope.ServiceProvider.GetRequiredService<ICommandRepository>();
                var platformPublishDto = JsonSerializer.Deserialize<PlatformPublishDto>(platformPublishMessage);

                try
                {
                    var platformModel = _mapper.Map<Platform>(platformPublishDto);
                    if(!repo.ExternalPlatformExists(platformModel.ExternalId))
                    {
                        Console.WriteLine($"Adding platform {platformModel.Name}");
                        repo.CreatePlatform(platformModel);
                        repo.SaveChanges();
                    }
                    else
                        Console.WriteLine($"Platform {platformModel.Name} already exists");
                }
                catch(Exception ex)
                {
                    Console.WriteLine($"Could NOT add platform {ex.Message}");
                }
            }
        }

        private EventType DetermineEventType(string message)
        {
            var eventType = JsonSerializer.Deserialize<GenericEventDto>(message);

            Console.WriteLine($"Event type {eventType.Event}");
            return eventType.Event;
        }
    }
}