namespace Mutual_17_Junio_Back.Models.Person
{
    public class PersonList
    {
        public int Id { get; set; }
        public int DniCuit { get; set; }
        public string? NombreRazonSocial { get; set; }
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }
        public string? Email { get; set; }
        public string Rol { get; set; }
    }

}
