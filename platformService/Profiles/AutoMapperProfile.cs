using AutoMapper;
using PaltformService.Controllers.Platforms.DTOs;
using platformService.Controllers.Platforms.DTOs;

namespace PlatformService.Profiles
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<PlatformCreateDto, Platform>();
            CreateMap<Platform, PlatformReadDto>();
            CreateMap<PlatformReadDto, PlatformPublishDto>();
        }
    }
}
