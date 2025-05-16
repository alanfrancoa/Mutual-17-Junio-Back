using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Mutual_17_Junio_Back.Data;
using Mutual_17_Junio_Back.Models;
using Mutual_17_Junio_Back.Models.User;

namespace Mutual_17_Junio_Back.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;

        public UserController(AppDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        [HttpPost("registro")]
        public async Task<IActionResult> RegistrarUsuario([FromBody] UserRegister registro)
        {
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(registro.PasswordHash);
            //var hashedPassword = registro.PasswordHash;

            var nuevaUsuario = new User
            {
                Username = registro.Username,
                PasswordHash = hashedPassword,
                Rol = registro.Rol,
            };

            _context.Users.Add(nuevaUsuario);
            await _context.SaveChangesAsync();

            return Ok(nuevaUsuario);
        }

        [HttpPost("login")]//Version de login q verifica contrseña hasheada, y retorna un token con los datos ingresado en Claim
        public async Task<IActionResult> Login([FromBody] UserLogin login)
        {
            var usuario = await _context.Users
                .FirstOrDefaultAsync(p => p.Username == login.Username);

            if (usuario == null)
                return Unauthorized(new { mensaje = "Usuario no encontrado" });

            if (!BCrypt.Net.BCrypt.Verify(login.PasswordHash, usuario.PasswordHash))
                return Unauthorized(new { mensaje = "Contraseña incorrecta" });

            var claims = new[]//Aca se ingresan los datos q quieras q se guarden en el token
            {
                new Claim("id", usuario.Id.ToString()),
                new Claim("username", usuario.Username),
                new Claim("rol", usuario.Rol)
            };

            var token = GenerarJwtToken(claims);

            return Ok(new { token });//solamente retornara un token q sera guardado temporalmente en una session
        }
        //[HttpPost("login")]//Version de login simpre
        //public async Task<IActionResult> Login([FromBody] UsuarioLogin login)
        //{
        //    var usuario = await _context.Usuarios
        //        .FirstOrDefaultAsync(p => p.Username == login.Username);

        //    if (usuario == null)
        //    {
        //        return Unauthorized(new { mensaje = "Usuario no encontrado" });
        //    }

        //    // Comparar hashes directamente si ya están hasheados
        //    if (usuario.PasswordHash != login.PasswordHash)
        //    {
        //        return Unauthorized(new { mensaje = "Contraseña incorrecta" });
        //    }

        //    // Opcional: Generar token JWT o retornar datos básicos
        //    return Ok(new
        //    {
        //        mensaje = "Login exitoso",
        //        usuario.Id,
        //        usuario.Username,
        //        usuario.Rol
        //    });
        //}

        //[HttpPost("login")]//Version de login q verifica contrseña hasheada
        //public async Task<IActionResult> Login([FromBody] UsuarioLogin login)
        //{
        //    var usuario = await _context.Usuarios
        //        .FirstOrDefaultAsync(p => p.Username == login.Username);

        //    if (usuario == null)
        //    {
        //        return Unauthorized(new { mensaje = "Usuario no encontrado" });
        //    }

        //    // Verificar la contraseña con BCrypt
        //    bool passwordValida = BCrypt.Net.BCrypt.Verify(login.PasswordHash, usuario.PasswordHash);

        //    if (!passwordValida)
        //    {
        //        return Unauthorized(new { mensaje = "Contraseña incorrecta" });
        //    }

        //    return Ok(new
        //    {
        //        usuario.Id,
        //        usuario.Username,
        //        usuario.Rol
        //    });
        //}
        private string GenerarJwtToken(IEnumerable<Claim> claims)//genera un token, y en este se ingresaran los datos q este en Claim
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
