using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiApiDB.Data;
using MiApiDB.Models;

namespace MiApiDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasInsumoController : ControllerBase
    {
        private readonly AppDbContext _context;
        public CategoriasInsumoController(AppDbContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoriaInsumo>>> GetCategoriasInsumo()
        {
            return await _context.CategoriasInsumo.ToListAsync();
        }
    }
}
