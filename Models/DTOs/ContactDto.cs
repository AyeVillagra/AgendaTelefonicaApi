namespace AgendaApi.Models.DTOs
{
    public class ContactDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CelularNumber { get; set; }
        public int? TelephoneNumber { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }

    }
}
