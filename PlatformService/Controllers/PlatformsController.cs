using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using PlatformService.Dtos;
using PlatformService.Models;

namespace PlatformService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private readonly IPlatformRepository _repository;
        private readonly IMapper _mapper;

        public PlatformsController(IPlatformRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDto>> GetAll()
        {
            Console.WriteLine("Getting platforms");
            var platforms = (from x in _repository.GetAll() select x).AsQueryable();

            return Ok(platforms.ProjectTo<PlatformReadDto>(_mapper.ConfigurationProvider));

            /*
                Or:
                    return Ok(_mapper.Map<PlatformAddDto>(platforms));
            */
        }

        [HttpGet("{id}", Name = "Get")]
        public ActionResult<PlatformReadDto> Get([FromRoute] int id)
        {
            Console.WriteLine($"Getting platform with id={id}");
            var platform = _repository.GetPlatformById(id);

            if (platform != null)
                return Ok(_mapper.Map<PlatformReadDto>(platform));

            return NotFound();
        }

        [HttpPost]
        public ActionResult<PlatformReadDto> Create([FromBody] PlatformAddDto platformAddDto)
        {
            Console.WriteLine("Adding platform");
            if (platformAddDto == null)
            {
                
            }
            
            var platform = _mapper.Map<Platform>(platformAddDto);

            _repository.Create(platform);
            _repository.SaveChanges();

            var platformReadDto = _mapper.Map<PlatformReadDto>(platform);
            
            return CreatedAtRoute(nameof(Get), new {platformReadDto.Id}, platformReadDto);
        }
    }
}