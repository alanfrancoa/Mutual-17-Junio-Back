using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
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

        [HttpPost("registro")]
        public async Task<IActionResult> RegistrarPersona([FromBody] PersonRegister registro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var nuevaPersona = new Person
            {
                DniCuit = registro.DniCuit,
                Rol = registro.Rol,
                NombreRazonSocial = registro.NombreRazonSocial,
                Direccion = registro.Direccion,
                Telefono = registro.Telefono,
                Email = registro.Email,

                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Activo = true
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


        [HttpGet("personas")]//Todas las personas
        public async Task<IActionResult> GetPersonas()
        {
            var personas = await _context.Persons.ToListAsync();
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
