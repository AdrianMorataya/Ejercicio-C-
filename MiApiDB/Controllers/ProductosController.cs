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
    public class ProductosController : ControllerBase
    {
        private readonly AppDbContext _context;
        public ProductosController(AppDbContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Producto>>> Get() =>
            await _context.Productos.Where(p => p.Activo).ToListAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<Producto>> Get(int id)
        {
            var item = await _context.Productos.FindAsync(id);
            return item == null ? NotFound() : item;
        }

        [HttpPost]
        public async Task<ActionResult<Producto>> Post(ProductoCrearDTO dto)
        {
            var item = new Producto
            {
                TipoId = dto.TipoId,
                Nombre = dto.Nombre,
                Precio = dto.Precio,
                Stock = dto.Stock,
                Activo = true  // lo seteamos por default
            };

            _context.Productos.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = item.ProductoId }, item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Producto item)
        {
            if (id != item.ProductoId) return BadRequest();
            _context.Entry(item).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Productos.AnyAsync(e => e.ProductoId == id)) return NotFound();
                throw;
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.Productos.FindAsync(id);
            if (item == null) return NotFound();

            // Eliminación lógica
            item.Activo = false;
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
