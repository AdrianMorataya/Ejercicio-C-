using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiApiDB.Data;
using MiApiDB.Models;

namespace MiApiDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoProductoController : ControllerBase
    {
        private readonly AppDbContext _context;
        public TipoProductoController(AppDbContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoProducto>>> GetTiposProducto()
        {
            return await _context.TiposProducto.ToListAsync();
        }
    }
}
