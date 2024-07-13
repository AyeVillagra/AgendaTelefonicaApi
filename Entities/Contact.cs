using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgendaApi.Entities
{
    public class Contact
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Number> Numbers { get; set; } = new List<Number>();
        public string Description { get; set; }
        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public User User { get; set; } //  relación de navegación: no se configura como una columna en la tabla sino que permite ir desde el userid (FK) al User correspondiente

    }
}
