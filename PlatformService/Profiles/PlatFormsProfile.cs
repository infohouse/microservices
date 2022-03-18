using AutoMapper;
using PlatformService.Dtos;
using PlatformService.Models;

namespace PlatformService.Profiles
{
    public class PlatFormsProfile : Profile
    {
        public PlatFormsProfile()
        {
            CreateMap<Platform, PlatformReadDto>();   
            CreateMap<PlatformCreateDto, Platform>();   
        }
    }
}