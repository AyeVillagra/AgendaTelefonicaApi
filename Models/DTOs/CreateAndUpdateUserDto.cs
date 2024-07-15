using AgendaApi.Models.Enum;
using System.ComponentModel.DataAnnotations;

namespace AgendaApi.Models.DTOs
{
    public class CreateAndUpdateUserDto

    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Name { get; set; }

        [Required(ErrorMessage = "El apellido es obligatorio")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [StringLength(15, MinimumLength = 8, ErrorMessage = "La contraseña debe tener entre 8 y 15 caracteres")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$", ErrorMessage = "La contraseña debe contener al menos una mayúscula, una minúscula y un número")]
        public string Password { get; set; }

        [Required(ErrorMessage = "El correo electrónico es obligatorio")]
        [EmailAddress(ErrorMessage = "Correo electrónico inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El nombre de usuario es obligatorio")]
        public string UserName { get; set; }

        public State State { get; set; }


        public Rol Rol { get; set; } = Rol.User;



    }
}
