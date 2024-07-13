namespace AgendaApi.Models.DTOs
{
    public class ContactDto
    {
        public int Id { get; set; }
        public string Name { get; set; }       
        public string Description { get; set; }
        public int UserId { get; set; }
        public List<NumberDto> Numbers { get; set; } = new List<NumberDto>();


        public ContactDto() 
        {
            
            Name = string.Empty;
            Description = string.Empty;
        }
    }
}
