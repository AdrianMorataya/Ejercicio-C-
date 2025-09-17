using Microsoft.AspNetCore.Mvc;
using MiApiDB.Data;
using MiApiDB.Dtos;
using MiApiDB.Models;
using MiApiDB.Helpers;

namespace MiApiDB.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly JwtService _jwtService;

        public UsuariosController(AppDbContext context, JwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            if (_context.Usuarios.Any(u => u.Correo == dto.Correo))
            {
                return BadRequest("El correo ya está en uso.");
            }

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            var usuario = new Usuario
            {
                ClienteId = dto.ClienteId,
                Nombre = dto.Nombre,
                Apellido = dto.Apellido,
                Correo = dto.Correo,
                PasswordHash = passwordHash
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                usuario.UsuarioId,
                usuario.Nombre,
                usuario.Apellido,
                usuario.Correo
            });
        }
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto dto)
        {
            var usuario = _context.Usuarios.SingleOrDefault(u => u.Correo == dto.Correo);

            if (usuario == null || !BCrypt.Net.BCrypt.Verify(dto.Password, usuario.PasswordHash))
            {
                return Unauthorized("Credenciales inválidas");
            }

            var token = _jwtService.GenerateToken(usuario.Correo);

            return Ok(new { token });
        }
    }
}
