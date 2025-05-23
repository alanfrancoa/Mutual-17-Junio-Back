using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mutual_17_Junio_Back.Models.User
{

    /* -------------------------- Modelo: User [12-05-2025] -------------------------- */

    /* Creacion de la tabla Usuario para la Base de Datos */
    [Table("USUARIO")]
    public class User
    {
        /* Primary Key*/
        [Key] 
        [Column("id")]
        public int Id { get; set;}

        [Column("username")]
        public string Username { get; set; }

        [Column("password_hash")]
        public string PasswordHash { get; set; }

        [Column("rol")]
        public string Rol { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("updated_at")]
        public DateTime? UpdateAt { get; set; }
    }
}
