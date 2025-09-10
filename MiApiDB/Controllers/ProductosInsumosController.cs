using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiApiDB.Data;
using MiApiDB.Models;

namespace MiApiDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosInsumosController : ControllerBase
    {
        private readonly AppDbContext _context;
        public ProductosInsumosController(AppDbContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductoInsumo>>> GetProductosInsumos()
        {
            return await _context.ProductosInsumos.ToListAsync();
        }
    }
}
