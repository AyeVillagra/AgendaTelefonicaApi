using AgendaApi.Entities;
using AgendaApi.Models.DTOs;

namespace AgendaApi.Data.Repository.Interfaces
{
    public interface IUserRepository
    {
        public User? ValidateUser(AuthenticationRequestBody authRequestBody);
        public User? GetById(int userId);
        public List<User> GetAll();
        public void Create(CreateAndUpdateUserDto dto);
        public User Update(CreateAndUpdateUserDto dto);
        public void Delete(int id);
        public void Archive(int id);
    }
}
