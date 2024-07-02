using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaltformService.AsyncDataServices;
using PaltformService.Controllers.Platforms.DTOs;
using PaltformService.SyncDataServices.Http;
using platformService.Controllers.Platforms.DTOs;
using PlatformService;
using PlatformService.IReposatiory;

namespace platformService.Controllers.Platforms
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformController : ControllerBase
    {
        private readonly IPlatformRepository _platformRepository;
        private readonly IMapper _mapper;   
        private readonly ICommandDataClient _commandDataClient;
        private readonly IMessageBusClient _messageBusClient;

        public PlatformController(
            IPlatformRepository platformRepository,
            IMapper mapper ,
            ICommandDataClient commandDataClient,
            IMessageBusClient messageBusClient)
        {
            _platformRepository=platformRepository;
            _mapper=mapper; 
            _commandDataClient=commandDataClient;
            _messageBusClient=messageBusClient;
        }

        [HttpGet]   

        public async Task<IActionResult> GetAllAsync()
        {
            var entities = await _platformRepository.GetAllAsync();
            var platforms = _mapper.Map<List<PlatformReadDto>>(entities);
            return Ok(platforms);
        }

        [HttpGet("{Id}")]

        public async Task<IActionResult> GetByIdAsync(Guid  Id )
        {
            var entity = await _platformRepository.GetByIdAsync(Id);
            if( entity is not null )
            {
                var platform = _mapper.Map<PlatformReadDto>(entity);
                return Ok(platform);
            }
            return NotFound();
       
        }


        [HttpPost]

        public async Task<IActionResult> CreateAsync(PlatformCreateDto platform)
        {
            var entity = _mapper.Map<Platform>(platform);
            await _platformRepository.CreateAsync(entity);
            await _platformRepository.SaveChangesAsync();
            var platformReadDto=_mapper.Map<PlatformReadDto>(entity);
            //#region sync message
            //try
            //{
            //    await _commandDataClient.SendPlatformService(platformReadDto);
            //    Console.WriteLine("the sync to command service created successfully ");

            //}
            //catch (Exception ex)
            //{

            //    Console.WriteLine(ex.Message);
            //}
            //#endregion

            try
            {
                var platformPublishDto=_mapper.Map<PlatformPublishDto>(platformReadDto);
                platformPublishDto.Event = "Platform_Published";
                 _messageBusClient.PublishNewPlatform(platformPublishDto);
                Console.WriteLine("the sync to command service created successfully ");

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            return Ok();

        }


    }
}
