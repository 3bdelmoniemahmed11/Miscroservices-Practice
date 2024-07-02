using AutoMapper;
using CommandsService.Controllers.DTOs;
using CommandsService.IRepositories;
using CommandsService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CommandsService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommandsController : ControllerBase
    {

        private readonly ICommandRepository _commandRepository;
        private readonly IMapper _mapper;
        public CommandsController(ICommandRepository commandRepository, IMapper mapper)
        {
            _commandRepository= commandRepository;  
            _mapper= mapper;    
        }

        [HttpGet("platform")]

        public async Task<ActionResult<List<CommandReadDto>>> GetAllCommandsByPlatformIdAsync(Guid platfromId)
        {
            Console.WriteLine("-----------------Getting all commands fro specific platfrom-------------------------");
            var entities = await _commandRepository.GetCommandsForPlatformAsync(platfromId);
            var commands = _mapper.Map<List<CommandReadDto>>(entities);
            return Ok(commands);
        }


        [HttpGet("command")]

        public async Task<ActionResult<CommandReadDto>> GetCommandsForPlatformAsync(Guid platfromId,Guid commandId)
        {
            Console.WriteLine("-----------------Getting a command for  specific platfrom-------------------------");
            var entities = await _commandRepository.GetCommandAsync(platfromId,commandId);
            var commands = _mapper.Map<CommandReadDto>(entities);
            return Ok(commands);
        }


        [HttpPost("createCommand")]

        public async Task<ActionResult<CommandCreateDto>> CreateCommandForPlatformAsync(CommandCreateDto command)
        {
            Console.WriteLine("-----------------Creating a command for  specific platfrom-------------------------");
            var platform =await  _commandRepository.GetPlatformByIdAsync(command.platformId);
            if(platform is not null )
            {
                var entity = _mapper.Map<Command>(command);
                entity.PlatformId = platform.Id;
                await _commandRepository.CreateCommandAsync(entity);
                return Ok();
            }

            return NotFound();
         
        }
    }
}
