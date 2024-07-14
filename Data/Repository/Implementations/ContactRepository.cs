using AgendaApi.Data.Repository.Interfaces;
using AgendaApi.Entities;
using AgendaApi.Models.DTOs;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AgendaApi.Data.Repository.Implementations
{
    public class ContactRepository : IContactRepository
    {
        private readonly AgendaContext _context;
        private readonly IMapper _mapper;
        private readonly INumberRepository _numberRepository;


        public ContactRepository(AgendaContext context, IMapper autoMapper, INumberRepository numberRepository)
        {
            _context = context;
            _mapper = autoMapper;
            _numberRepository = numberRepository;
        }

        public Contact GetContactById(int id, int userId)
        {
            // Buscar el contacto por ID y pertenencia al usuario
            Contact contact = _context.Contacts
                .Include(c => c.Numbers)
                .SingleOrDefault(c => c.Id == id && c.UserId == userId);
            

            // Si el contacto no existe o no pertenece al usuario, lanzar una excepción o devolver null
            if (contact == null)
            {
                // Puedes elegir lanzar una excepción NotFound o simplemente devolver null
                throw new Exception("Contact not found or does not belong to the user."); // Lanzar una excepción
                                                                                          // Opción alternativa: return null; // Devolver null
            }

            // Devolver el contacto encontrado
            return contact;
        }
        public List<Contact> GetAllByUser(int id)
        {
            return _context.Contacts
                .Where(c => c.UserId == id)
                .Include(c => c.Numbers)
                .ToList();
        }

        public Contact Create(CreateAndUpdateContactDto dto, List<NumberDto> numbersDto, int UserId)
        {
            // Mapea el DTO a la entidad Contact
            Contact contact = _mapper.Map<Contact>(dto);

            // Asigna el UserId
            contact.UserId = UserId;

            // Agrega el contacto al contexto y guarda los cambios en la base de datos
            _context.Contacts.Add(contact);
            _context.SaveChanges();

            foreach (var numberDto in dto.Numbers)
            {
                var number = _mapper.Map<Number>(numberDto);
                number.ContactId = contact.Id; // Asigna el Id del contacto al número
                _context.Numbers.Add(number);
            }
            // _context.SaveChanges();
            // Devuelve el contacto mapeado a DTO
            // Contact contactDto = _mapper.Map<Contact>(contact);
            // return contactDto;            
            return contact;
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
            Contact existingContact = _context.Contacts
               .Include(c => c.Numbers)
               .SingleOrDefault(c => c.Id == dto.Id && c.UserId == userId);

            if (existingContact != null)
            {
                // mapea el dto recibido como parámetro con el contacto encontrado en la db
                _mapper.Map(dto, existingContact);

                // Eliminar números que no están en el dto que llega desde el front                
                foreach (var number in existingContact.Numbers.ToList())
                {
                    var numberDto = dto.Numbers.FirstOrDefault(n => n.Id == number.Id);
                    if (numberDto == null)
                    {
                        _numberRepository.DeleteNumber(number.Id);
                    }
                }

                // Actualizar o agregar nuevos números
                foreach (var numberDto in dto.Numbers)
                {
                    // numero nuevo
                    if (numberDto.Id == 0 || numberDto.Id == null)
                    {
                        _numberRepository.AddNumber(numberDto, existingContact.Id);
                    }
                    else
                    {
                        //  actualizar un número existente
                        _numberRepository.UpdateNumber(numberDto);
                    }
                }

                _context.SaveChanges();
            }
        }

    }
}
