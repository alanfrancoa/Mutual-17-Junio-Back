using System.ComponentModel.DataAnnotations;

namespace Mutual_17_Junio_Back.Models.User
{

    /* -------------------------- Modelo: UserRegister [12-05-2025] -------------------------- */

    /* Creacion del modelo para el registro de Usuarios */
    public class UserRegister
    {

        /* Validaciones necesarias para el registro del nuevo Usuario  */

        [Required(ErrorMessage = "El nombre de usuario es obligatorio.")]
        [MinLength(6, ErrorMessage = "El nombre de usuario debe tener al menos 6 caracteres.")]
        [RegularExpression(@"^(?=.*[0-9\W]).+$", ErrorMessage = "El nombre de usuario debe incluir números o símbolos.")]
        public required string Username { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [MinLength(8, ErrorMessage = "La contraseña debe tener al menos 8 caracteres.")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*[\d\W]).+$", ErrorMessage = "La contraseña debe contener letras y al menos un número o símbolo.")]
        public required string PasswordHash { get; set; }

        [Required]
        [Compare("PasswordHash", ErrorMessage = "Las contraseñas no coinciden.")]
        public required string ConfirmarPassword { get; set; }

        [Required(ErrorMessage = "El rol es obligatorio.")]
        [RegularExpression("^(Admin|Gestor|Consultor)$", ErrorMessage = "Rol inválido. Solo se permiten: Admin, Gestor, Consultor.")]
        public string Rol { get; set; } = "Admin";
    }
}
