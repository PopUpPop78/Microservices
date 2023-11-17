using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common.Dtos;
using Common.Events;
using Microsoft.AspNetCore.Mvc;
using PlatformService.AsyncDataServices;
using PlatformService.Data;
using PlatformService.Dtos;
using PlatformService.Models;
using PlatformService.SyncDataServices.Http;

namespace PlatformService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private readonly IPlatformRepository _repository;
        private readonly IMapper _mapper;
        private readonly ICommandDataClient _commandClient;
        private readonly IMessageBusClient _messageBusClient;

        public PlatformsController(
            IPlatformRepository repository, 
            IMapper mapper, 
            ICommandDataClient commandClient, 
            IMessageBusClient messageBusClient)
        {
            _repository = repository;
            _mapper = mapper;
            _commandClient = commandClient;
            _messageBusClient = messageBusClient;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDto>> GetAll()
        {
            Console.WriteLine("Getting platforms");
            var platforms = (from x in _repository.GetAll() select x).ToList();
            
            return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(platforms));
        }

        [HttpGet("{id}", Name = "Get")]
        public ActionResult<PlatformReadDto> Get([FromRoute] int id)
        {
            Console.WriteLine($"Getting platform with id={id}");
            var platform = _repository.GetItemById(id);

            if (platform != null)
                return Ok(_mapper.Map<PlatformReadDto>(platform));

            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<PlatformReadDto>> Create([FromBody] PlatformAddDto platformAddDto)
        {
            Console.WriteLine("Adding platform");
            if (platformAddDto == null)
            {
                return NotFound(platformAddDto);
            }
            
            var platform = _mapper.Map<Platform>(platformAddDto);

            _repository.Create(platform);
            _repository.SaveChanges();

            var platformReadDto = _mapper.Map<PlatformReadDto>(platform);

            try
            {
                await _commandClient.SendPlatformToCommand(platformReadDto);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error occured sending to command service {ex.Message}");
            }            
                
            try
            {
                var platformPublishDto = _mapper.Map<PlatformPublishDto>(platformReadDto);
                platformPublishDto.Event = EventType.PlatformPublish;

                _messageBusClient.PublishNewPlatform(platformPublishDto);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Could not publish platform {ex.Message}");
            }
            
            return CreatedAtRoute(nameof(Get), new {platformReadDto.Id}, platformReadDto);
        }
    }
}