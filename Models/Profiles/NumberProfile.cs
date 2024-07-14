using AgendaApi.Entities;
using AgendaApi.Models.DTOs;
using AutoMapper;

namespace AgendaApi.Models.Profiles
{
    public class NumberProfile : Profile
    {
        public NumberProfile()

        {
            CreateMap<NumberDto, Number>();
            CreateMap<Number, NumberDto>();
        }
    }
}
