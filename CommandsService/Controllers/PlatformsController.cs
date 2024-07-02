using AutoMapper;
using CommandsService.Controllers.DTOs;
using CommandsService.IRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CommandsService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {

        private readonly ICommandRepository _commandRepository;
        private readonly IMapper _mapper;

        public PlatformsController(ICommandRepository commandRepository, IMapper mapper)
        {
            _commandRepository = commandRepository;
            _mapper = mapper;

        }

        [HttpGet]
        public async Task<ActionResult<List<PlatformReadDto>>> GetAllPlatformsAsync()
        {
            Console.WriteLine("-----------------Getting all platforms-------------------------");
            var entities = await _commandRepository.GetAllPlatformsAsync();
            var platforms = _mapper.Map<List<PlatformReadDto>>(entities);
            return Ok(platforms);
        }



        [HttpPost]
        public IActionResult TestInboundedConnection()
        {
            return Ok ("Inbound Connection Created Successfully");
        }
    }
}
