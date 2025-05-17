using Microsoft.EntityFrameworkCore;
using Mutual_17_Junio_Back.Models.User;
using Mutual_17_Junio_Back.Models.Person;

namespace Mutual_17_Junio_Back.Data
{

    /* -------------------------- Data: AppDbContext [12-05-2025] -------------------------- */

    /* Creando la conexión entre la API y la Base de Datos */
    public class AppDbContext : DbContext
    {

      public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        
        /* Tablas de la Base de datos */
        public DbSet<User> Users { get; set; }

        public DbSet<Person> Persons { get; set; }
    }
}
