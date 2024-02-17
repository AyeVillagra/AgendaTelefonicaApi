using AgendaApi.Entities;
using AgendaApi.Models.DTOs;
using AutoMapper;

namespace AgendaApi.Models.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, CreateAndUpdateUserDto>();            
            CreateMap<User, UserDto>();
            CreateMap<CreateAndUpdateUserDto, User>();
            CreateMap<UserDto, User>();
        }

    }
}
