using AgendaApi.Models.Enum;
using System.ComponentModel.DataAnnotations;

namespace AgendaApi.Models.DTOs
{
    public class CreateAndUpdateUserDto

    {
        public int Id { get; set; }
        
        public string Name { get; set; } 
        
        public string LastName { get; set; } 
        
        public string Password { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string UserName { get; set; }
        

        public Rol rol { get; set; } = Rol.User;
       
    }
}
