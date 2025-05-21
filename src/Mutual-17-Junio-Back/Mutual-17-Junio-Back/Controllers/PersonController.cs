using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Differencing;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Mutual_17_Junio_Back.Data;
using Mutual_17_Junio_Back.Models.Person;

namespace Mutual_17_Junio_Back.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;

        public PersonController(AppDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        //[HttpPost("registro")]
        //public async Task<IActionResult> RegistrarPersona([FromBody] PersonRegister registro)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var nuevaPersona = new Person
        //    {
        //        DniCuit = registro.DniCuit,
        //        Rol = registro.Rol,
        //        NombreRazonSocial = registro.NombreRazonSocial,
        //        Direccion = registro.Direccion,
        //        Telefono = registro.Telefono,
        //        Email = registro.Email,

        //        CreatedAt = DateTime.Now,
        //        UpdatedAt = DateTime.Now,
        //        Activo = true
        //    };

        //    _context.Persons.Add(nuevaPersona);
        //    await _context.SaveChangesAsync();

        //    return Ok(nuevaPersona);
        //}

        [Authorize(Roles = "Admin")]
        [HttpPost("registro")]
        public async Task<IActionResult> RegistrarPersona([FromBody] PersonRegister registro)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Obtener el ID del usuario desde el token
            var idUsuario = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");

            // Crear la persona
            var nuevaPersona = new Person
            {
                DniCuit = registro.DniCuit,
                Rol = registro.Rol,
                NombreRazonSocial = registro.NombreRazonSocial,
                Direccion = registro.Direccion,
                Telefono = registro.Telefono,
                Email = registro.Email,
                CreatedAt = DateTime.Now,
                Activo = true,
                CreatedBy = idUsuario.ToString(), // Guardar el ID del usuario que creó la persona
            };

            _context.Persons.Add(nuevaPersona);
            await _context.SaveChangesAsync();

            return Ok(nuevaPersona);
        }


        [HttpPut("personas/{id}")]//Actulizar persona x id
        public async Task<IActionResult> ActualizarPersona(int id, [FromBody] PersonRegister actualizacion)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var persona = await _context.Persons.FindAsync(id);
            if (persona == null)
                return NotFound(new { mensaje = "Persona no encontrada" });

            persona.DniCuit = actualizacion.DniCuit;
            persona.Rol = actualizacion.Rol;
            persona.UpdatedAt = DateTime.Now;

            _context.Persons.Update(persona);
            await _context.SaveChangesAsync();

            return Ok(new { mensaje = "Persona actualizada correctamente", persona });
        }


        //[HttpGet("personas")]//Todas las personas y todos sus datos
        //public async Task<IActionResult> GetPersonas()
        //{
        //    var personas = await _context.Persons.ToListAsync();
        //    return Ok(personas);
        //}

        [HttpGet("personas")]//Todas las personas y datos eleigos en PersonList
        public async Task<IActionResult> GetPersonas()
        {
            var personas = await _context.Persons
                .Select(p => new PersonList
                {
                    Id = p.Id,
                    DniCuit = p.DniCuit,
                    NombreRazonSocial = p.NombreRazonSocial,
                    Direccion = p.Direccion,
                    Telefono = p.Telefono,
                    Email = p.Email,
                    Rol = p.Rol
                })
                .ToListAsync();

            return Ok(personas);
        }

        [HttpGet("personas/{id}")]//Una persona x id
        public async Task<IActionResult> GetPersonaPorId(int id)
        {
            var persona = await _context.Persons.FindAsync(id);
            if (persona == null)
                return NotFound(new { mensaje = "Persona no encontrada" });

            return Ok(persona);
        }

    }
}
