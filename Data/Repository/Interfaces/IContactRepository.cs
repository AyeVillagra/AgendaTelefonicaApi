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
        public Contact GetContactById(int id);
    }
}
