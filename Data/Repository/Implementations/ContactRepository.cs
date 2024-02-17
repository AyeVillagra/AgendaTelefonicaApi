using AgendaApi.Data.Repository.Interfaces;
using AgendaApi.Entities;
using AgendaApi.Models.DTOs;
using AutoMapper;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace AgendaApi.Data.Repository.Implementations
{
    public class ContactRepository : IContactRepository
    {
        private readonly AgendaContext _context;
        private readonly IMapper _mapper;

        public ContactRepository(AgendaContext context, IMapper autoMapper)
        {
            _context = context;
            _mapper = autoMapper;
        }

        public Contact GetContactById(int id)
        {
            return _context.Contacts
                .Single(c => c.Id == id);
        }
        public List<ContactDto> GetAllByUser(int id)
        {

            return _context.Contacts.Include(c => c.User).Where(c => c.User.Id == id).Select(contact => new ContactDto()
            {
                Id = contact.Id,                
                Description = contact.Description,                
                Name = contact.Name,
                CelularNumber = contact.CelularNumber,
                TelephoneNumber = contact.TelephoneNumber,
                UserId = contact.UserId
            }).ToList();
        }

        public Contact Create(CreateAndUpdateContactDto dto, int UserId)
        {
            // Mapea el DTO a la entidad Contact
            Contact contact = _mapper.Map<Contact>(dto);

            // Asigna el UserId
            contact.UserId = UserId;

            // Agrega el contacto al contexto y guarda los cambios en la base de datos
            _context.Contacts.Add(contact);
            _context.SaveChanges();

            // Devuelve el contacto mapeado a DTO
            Contact contactDto = _mapper.Map<Contact>(contact);
            return contactDto;
        }

        public void Delete(int id)
        {
            _context.Contacts.Remove(_context.Contacts.Single(c => c.Id == id));
            _context.SaveChanges();
        }


        public void Update(CreateAndUpdateContactDto dto)
        {
            _context.Contacts.Update(_mapper.Map<Contact>(dto));
            _context.SaveChanges();
        }
    }
}
