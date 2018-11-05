using AutoMapper;
using OA.WebApp.ViewModels;
using OA.WebApp.Models;
using System.Data;

namespace OA.Core.Tools
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();

            CreateMap<IDataReader, PrivilegeDto>();
            CreateMap<Privilege, PrivilegeDto>();
            CreateMap<PrivilegeDto, Privilege>();

            //lxy
            CreateMap<Journal, JournalDto>();
            CreateMap<JournalDto, Journal>();
        }
    }
}