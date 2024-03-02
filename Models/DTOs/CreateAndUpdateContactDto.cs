using AgendaApi.Entities;
using System.ComponentModel.DataAnnotations;

namespace AgendaApi.Models.DTOs
{
    public class CreateAndUpdateContactDto        
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int? CelularNumber { get; set; }
        public int? TelephoneNumber { get; set; }
        public string Description { get; set; }    
    }
}
