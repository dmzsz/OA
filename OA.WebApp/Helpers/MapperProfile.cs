using AutoMapper;
using OA.WebApp.ViewModels;
using OA.WebApp.Models;

namespace OA.Core.Tools
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
        }
    }
}