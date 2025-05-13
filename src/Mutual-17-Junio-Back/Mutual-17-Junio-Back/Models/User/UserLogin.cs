namespace Mutual_17_Junio_Back.Models.User
{

    /* -------------------------- Modelo: UserLogin [12-05-2025] -------------------------- */

    /* Creacion del modelo para el logueo de Usuarios */
    public class UserLogin
    {
        public required string Username { get; set; }
        public required string PasswordHash { get; set; }
    }
}
