using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiApiDB.Data;
using MiApiDB.Helpers;
using MiApiDB.Models;
using MiApiDB.Dtos;
using Microsoft.AspNetCore.Identity;


namespace MiApiDB.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly JwtService _jwt;

        public UsuariosController(AppDbContext context, JwtService jwt)
        {
            _context = context;
            _jwt = jwt;
        }

        // 🔹 Login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Correo == dto.Correo);
            if (usuario == null)
                return Unauthorized("Usuario o contraseña incorrecta");

            var passwordHasher = new PasswordHasher<Usuario>();
            var result = passwordHasher.VerifyHashedPassword(usuario, usuario.PasswordHash, dto.Password);

            if (result == PasswordVerificationResult.Failed)
                return Unauthorized("Usuario o contraseña incorrecta");

            var token = _jwt.GenerateToken(usuario.Correo, usuario.Rol);
            return Ok(new { token });
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            if (await _context.Usuarios.AnyAsync(u => u.Correo == dto.Correo))
                return BadRequest(new { message = "El correo ya existe" });

            var hasher = new PasswordHasher<Usuario>();

            var nuevoUsuario = new Usuario
            {
                Nombre = dto.Nombre,
                Apellido = dto.Apellido,
                Correo = dto.Correo,
                Rol = dto.Rol ?? "Empleado"
            };

            nuevoUsuario.PasswordHash = hasher.HashPassword(nuevoUsuario, dto.Password);

            _context.Usuarios.Add(nuevoUsuario);
            await _context.SaveChangesAsync();

            // Devuelve un objeto JSON
            return Ok(new { message = "Usuario registrado correctamente", correo = nuevoUsuario.Correo });
        }
    }
}
