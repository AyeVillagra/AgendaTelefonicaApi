using AgendaApi.Models.Enum;
using System.ComponentModel.DataAnnotations;

namespace AgendaApi.Models.DTOs
{
    public class CreateAndUpdateNumberDto
    {
        public int Id { get; set; } 
        [Required]
        public string ContactNumber { get; set; }
        [Required]
        public NumberType Type { get; set; }
    }
}
