using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiApiDB.Data;
using MiApiDB.Models;

namespace MiApiDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly AppDbContext _context;
        public UsuariosController(AppDbContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
            return await _context.Usuarios.ToListAsync();
        }
    }
}
