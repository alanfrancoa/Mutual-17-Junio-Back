using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Mutual_17_Junio_Back.Models.Person
{
    //Creacion de modelo para el registro de Personas
    public class PersonRegister
    {
        [Required(ErrorMessage = "El nombre o razón social es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre o razón social no puede tener más de 100 caracteres.")]
        public string NombreRazonSocial { get; set; }

        [Required(ErrorMessage = "La dirección es obligatoria.")]
        [StringLength(200, ErrorMessage = "La dirección no puede tener más de 200 caracteres.")]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "El teléfono es obligatorio.")]
        [RegularExpression(@"^\+?[0-9\s\-]{6,20}$", ErrorMessage = "El teléfono tiene un formato inválido.")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "El email es obligatorio.")]
        [EmailAddress(ErrorMessage = "El email tiene un formato inválido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El DNI/CUIT es obligatorio.")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "El DNI/CUIT solo puede contener números.")]
        public int DniCuit { get; set; }

        [Required(ErrorMessage = "El rol es obligatorio.")]
        [RegularExpression("^(Asociado|Familiar|Proveedor)$", ErrorMessage = "Rol inválido. Solo se permiten: Admin, Socio, Proovedor.")]
        public string Rol { get; set; }
    }
}
