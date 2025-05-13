using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mutual_17_Junio_Back.Models.User
{

    /* -------------------------- Modelo: User [12-05-2025] -------------------------- */

    /* Creacion de la tabla Usuario para la Base de Datos */
    [Table("Usuario")]
    public class User
    {

        /* Primary Key*/
        [Key] 
        [Column("id")]
        public int Id { get; set;}

        [Column("username")]
        public int Username { get; set; }

        [Column("password_hash")]
        public int PasswordHash { get; set; }

        [Column("rol")]
        public int Rol { get; set; }

    }
}
