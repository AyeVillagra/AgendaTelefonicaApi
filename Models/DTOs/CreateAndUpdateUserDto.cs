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
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        [Required]
        public string UserName { get; set; }

        public State State { get; set; }


        public Rol Rol { get; set; } 

        

    }
}
