using AgendaApi.Entities;
using System.ComponentModel.DataAnnotations;

namespace AgendaApi.Models.DTOs
{
    public class CreateAndUpdateContactDto        
    {
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre es requerido")]
        public string Name { get; set; }
        public List<NumberDto> Numbers { get; set; } = new List<NumberDto>();
        public string Description { get; set; }
    }
}
