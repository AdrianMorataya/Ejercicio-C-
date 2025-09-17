using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task<ActionResult<IEnumerable<ProductoInsumo>>> Get() =>
            await _context.ProductosInsumos.ToListAsync();

        [HttpGet("{productoId}/{insumoId}")]
        public async Task<ActionResult<ProductoInsumo>> Get(int productoId, int insumoId)
        {
            var item = await _context.ProductosInsumos
                .FirstOrDefaultAsync(pi => pi.ProductoId == productoId && pi.InsumoId == insumoId);
            return item == null ? NotFound() : item;
        }

        [HttpPost]
        public async Task<ActionResult<ProductoInsumo>> Post(ProductoInsumo item)
        {
            _context.ProductosInsumos.Add(item);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { productoId = item.ProductoId, insumoId = item.InsumoId }, item);
        }

        [HttpPut("{productoId}/{insumoId}")]
        public async Task<IActionResult> Put(int productoId, int insumoId, ProductoInsumo item)
        {
            if (productoId != item.ProductoId || insumoId != item.InsumoId) return BadRequest();

            var existing = await _context.ProductosInsumos
                .FirstOrDefaultAsync(pi => pi.ProductoId == productoId && pi.InsumoId == insumoId);
            if (existing == null) return NotFound();

            existing.Cantidad = item.Cantidad;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{productoId}/{insumoId}")]
        public async Task<IActionResult> Delete(int productoId, int insumoId)
        {
            var item = await _context.ProductosInsumos
                .FirstOrDefaultAsync(pi => pi.ProductoId == productoId && pi.InsumoId == insumoId);
            if (item == null) return NotFound();
            _context.ProductosInsumos.Remove(item);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
