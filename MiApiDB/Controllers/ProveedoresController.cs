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
    public class ProveedoresController : ControllerBase
    {
        private readonly AppDbContext _context;
        public ProveedoresController(AppDbContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Proveedor>>> Get() =>
            await _context.Proveedores.ToListAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<Proveedor>> Get(int id)
        {
            var item = await _context.Proveedores.FindAsync(id);
            return item == null ? NotFound() : item;
        }

        [HttpPost]
        public async Task<ActionResult<Proveedor>> Post(Proveedor item)
        {
            _context.Proveedores.Add(item);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = item.ProveedorId }, item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Proveedor item)
        {
            if (id != item.ProveedorId) return BadRequest();
            _context.Entry(item).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Proveedores.AnyAsync(e => e.ProveedorId == id)) return NotFound();
                throw;
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.Proveedores.FindAsync(id);
            if (item == null) return NotFound();
            _context.Proveedores.Remove(item);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
