using AgendaApi.Entities;
using AgendaApi.Models.DTOs;
using AutoMapper;

namespace AgendaApi.Models.Profiles
{
    public class ContactProfile : Profile
    {
        public ContactProfile()
        {

            CreateMap<CreateAndUpdateContactDto, Contact>()
      .ForMember(dest => dest.Numbers, opt => opt.MapFrom(src => src.Numbers));

            CreateMap<Contact, CreateAndUpdateContactDto>()
                .ForMember(dest => dest.Numbers, opt => opt.MapFrom(src => src.Numbers));

            

        }
    }
}
