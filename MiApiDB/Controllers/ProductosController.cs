using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiApiDB.Data;
using MiApiDB.Models;

namespace MiApiDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly AppDbContext _context;
        public ProductosController(AppDbContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Producto>>> GetProductos()
        {
            return await _context.Productos.ToListAsync();
        }
    }
}
