using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mutual_17_Junio_Back.Models.Person
{
    //Clase de modelo para la tabla Persona
    [Table("Persona")]
    public class Person
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("dni_cuit")]
        public int DniCuit { get; set; }

        [Column("nombre_razon_social")]
        public string? NombreRazonSocial { get; set; }

        [Column("direccion")]
        public string? Direccion { get; set; }

        [Column("telefono")]
        public string? Telefono { get; set; }

        [Column("email")]
        public string? Email { get; set; }

        [Column("rol")]
        public string Rol { get; set; }

        [Column("organismo_id")]
        public int? OrganismoId { get; set; }

        [Column("categoria_id")]
        public int? CategoriaId { get; set; }

        [Column("fecha_afiliacion")]
        public DateTime? FechaAfiliacion { get; set; }

        [Column("activo")]
        public bool Activo { get; set; }

        [Column("personal_referencia_id")]
        public int? PersonalReferenciaId { get; set; }

        [Column("created_at")]
        public DateTime? CreatedAt { get; set; }

        [Column("update_at")]
        public DateTime? UpdatedAt { get; set; }

        [Column("created_by")]
        public string? CreatedBy { get; set; }
    }
}
