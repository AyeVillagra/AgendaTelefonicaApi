using AgendaApi.Models.Enum;

namespace AgendaApi.Models.DTOs
{
    public class NumberDto
    {
        public int Id { get; set; }
        public required string ContactNumber { get; set; }
        public NumberType Type { get; set; }
    }
}

