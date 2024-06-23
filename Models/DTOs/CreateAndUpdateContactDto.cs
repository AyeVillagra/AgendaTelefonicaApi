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
        [RegularExpression(@"^\d+$", ErrorMessage = "CelularNumber must be a number")]
        public int? CelularNumber { get; set; }
        [RegularExpression(@"^\d+$", ErrorMessage = "TelephoneNumber must be a number")]
        public int? TelephoneNumber { get; set; }
        public string Description { get; set; }    
    }
}
