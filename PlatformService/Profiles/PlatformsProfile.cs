using AutoMapper;
using Common.Dtos;
using PlatformService.Dtos;
using PlatformService.Models;

namespace PlatformService.Profiles
{
    public class PlatformsProfile : Profile
    {
        public PlatformsProfile()
        {
            // Source -> Target
            CreateMap<Platform, PlatformReadDto>();
            CreateMap<PlatformAddDto, Platform>();
            CreateMap<PlatformReadDto, PlatformPublishDto>();
        }
    }
}