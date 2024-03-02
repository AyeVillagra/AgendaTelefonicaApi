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
        public List<Contact> GetAllByUser(int id)
        {
            return _context.Contacts
                .Where(c => c.UserId == id)                
                .ToList();
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
        public bool ContactBelongsToUser(int contactId, int userId)
        {
            return _context.Contacts.Any(c => c.Id == contactId && c.UserId == userId);
        }
        public void Delete(int id)
        {
            Contact contactToDelete = _context.Contacts.SingleOrDefault(c => c.Id == id);

            if (contactToDelete != null)
            {
                _context.Contacts.Remove(contactToDelete);
                _context.SaveChanges();
            }
        }

        // El método toma un objeto DTO y lo mapea a una entidad, actualiza esa entidad
        // en el contexto (instancia de DbContext que representa el estado de las entidades) y luego guarda los cambios en la DB
        public void Update(CreateAndUpdateContactDto dto, int userId)
        {            
            Contact existingContact = _context.Contacts.SingleOrDefault(c => c.Id == dto.Id && c.UserId == userId);

            if (existingContact != null)
            {
                _mapper.Map(dto, existingContact);
                _context.SaveChanges();
            }
        }

    }
}
