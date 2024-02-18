using System.Xml.Linq;

namespace AgendaApi.Models.DTOs
{    
        public class UserDto
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public string UserName { get; set; }
    

            public UserDto()
            {
                // Inicializar propiedades para evitar advertencias CS8618
                Name = string.Empty;
                LastName = string.Empty;
                Email = string.Empty;
                Password = string.Empty;
                UserName = string.Empty;
            }
    }
}
