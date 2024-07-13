using AgendaApi.Entities;
using AgendaApi.Models.Enum;
using Microsoft.EntityFrameworkCore;

namespace AgendaApi.Data
{
    public class AgendaContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Number> Numbers { get; set; }

        public AgendaContext(DbContextOptions<AgendaContext> options) : base(options) //Acá estamos llamando al constructor de DbContext que es el que acepta las opciones
        {
            Users = Set<User>();
            Contacts = Set<Contact>();
            Numbers = Set<Number>();
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

            modelBuilder.Entity<User>().HasData(
                karen);
            

            modelBuilder.Entity<User>()
            .HasMany(u => u.Contacts)
            .WithOne(c => c.User)
            .HasForeignKey(c => c.UserId)  // establece la clave foránea
            .OnDelete(DeleteBehavior.Cascade); // si un user se elimina, se eliminan sus contactos

            modelBuilder.Entity<Contact>()
            .HasMany(c => c.Numbers)
            .WithOne(n => n.Contact)
            .HasForeignKey(n => n.ContactId)
            .OnDelete(DeleteBehavior.Cascade);


            base.OnModelCreating(modelBuilder);
        }
    }
}
