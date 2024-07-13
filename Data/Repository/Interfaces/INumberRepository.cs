using AgendaApi.Entities;
using AgendaApi.Models.DTOs;

namespace AgendaApi.Data.Repository.Interfaces
{
    public interface INumberRepository
    {
        List<Number> GetNumbersByContact(int contactId);
        Number AddNumber(NumberDto dto, int contactId);
        void UpdateNumber(NumberDto dto);
        void DeleteNumber(int numberId);
    }
}

