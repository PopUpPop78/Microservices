using AutoMapper;
using CommandService.Dtos;
using CommandService.Models;
using Common.Dtos;

namespace CommandService.Profiles
{
    public class CommandsProfile : Profile
    {
        public CommandsProfile()
        {
            // Source -> Target
            CreateMap<Platform, PlatformReadDto>();
            CreateMap<CommandCreateDto, Command>();
            CreateMap<Command, CommandReadDto>();
            CreateMap<PlatformPublishDto, Platform>()
                .ForMember(d=> d.ExternalId, opt => opt.MapFrom(src=> src.Id));
            CreateMap<GrpcPlatformModel, Platform>()
                .ForMember(d=> d.ExternalId, opt => opt.MapFrom(src=> src.PlatformId))
                .ForMember(d=> d.Name, opt => opt.MapFrom(src=> src.Name))
                .ForMember(d=> d.Commands, opt => opt.Ignore());
        }
    }
}