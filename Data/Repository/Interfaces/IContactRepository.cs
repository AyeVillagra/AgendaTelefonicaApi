using AgendaApi.Entities;
using AgendaApi.Models.DTOs;

namespace AgendaApi.Data.Repository.Interfaces
{
    public interface IContactRepository
    {
        public List<Contact> GetAllByUser(int Id);
        Contact Create(CreateAndUpdateContactDto dto, int UserId);        
        public void Update(CreateAndUpdateContactDto dto, int UserId);
        public void Delete(int id);
        bool ContactBelongsToUser(int contactId, int userId);
        Contact GetContactById(int id, int userId);
    }
}
