using AgendaApi.Data.Repository.Interfaces;
using AgendaApi.Entities;
using AgendaApi.Models.DTOs;
using AutoMapper;

namespace AgendaApi.Data.Repository.Implementations
{
    public class NumberRepository : INumberRepository
    {
        private readonly AgendaContext _context;
        private readonly IMapper _mapper;

        public NumberRepository(AgendaContext context, IMapper autoMapper)
        {
            _context = context;
            _mapper = autoMapper;
        }

        public List<Number> GetNumbersByContact(int contactId)
        {
            return _context.Numbers
                .Where(n => n.ContactId == contactId)
                .ToList();
        }

        public Number AddNumber(NumberDto dto, int contactId)
        {
            var number = _mapper.Map<Number>(dto);
            number.ContactId = contactId;
            _context.Numbers.Add(number);
            
            return number;
        }

        public void UpdateNumber(NumberDto dto)
        {
            var number = _context.Numbers.SingleOrDefault(n => n.Id == dto.Id);
            if (number != null)
            {
                _mapper.Map(dto, number);
                
            }
        }

        public void DeleteNumber(int numberId)
        {
            var number = _context.Numbers.SingleOrDefault(n => n.Id == numberId);
            if (number != null)
            {
                _context.Numbers.Remove(number);              
            }
        }
    }
}

