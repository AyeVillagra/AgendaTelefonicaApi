using AgendaApi.Entities;
using AgendaApi.Models.Enum;
using Microsoft.EntityFrameworkCore;

namespace AgendaApi.Data
{
    public class AgendaContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Contact> Contacts { get; set; }

        public AgendaContext(DbContextOptions<AgendaContext> options) : base(options) //Acá estamos llamando al constructor de DbContext que es el que acepta las opciones
        {
            Users = Set<User>();
            Contacts = Set<Contact>();            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            User karen = new User()
            {
                Id = 1,
                Name = "Karen",
                LastName = "Lasot",
                Password = "Pa$$w0rd",
                Email = "prueba@gmail.com",
                UserName = "karen",
                Rol = Rol.Admin,
                state = State.Active
            };


            Contact jaimitoC = new Contact()
            {
                Id = 1,
                Name = "Jaimito",                
                CelularNumber = 341457896,
                Description = "Plomero",
                TelephoneNumber = null,
                UserId = karen.Id,
            };

            Contact pepeC = new Contact()
            {
                Id = 2,
                Name = "Pepe",                
                CelularNumber = 34156978,
                Description = "Papa",
                TelephoneNumber = 422568,
                UserId = karen.Id,
            };            

            modelBuilder.Entity<User>().HasData(
                karen);

            modelBuilder.Entity<Contact>().HasData(
                 jaimitoC,pepeC
                 );

            modelBuilder.Entity<User>()
            .HasMany(u => u.Contacts)
            .WithOne(c => c.User)
            .HasForeignKey(c => c.UserId)  // Especifica la clave externa
            .OnDelete(DeleteBehavior.Cascade); // si un user se elimina, se eliminan sus contactos

            base.OnModelCreating(modelBuilder);
        }
    }
}
