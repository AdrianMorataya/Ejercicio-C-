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
    public class VentasController : ControllerBase
    {
        private readonly AppDbContext _context;
        public VentasController(AppDbContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Venta>>> Get() =>
            await _context.Ventas.ToListAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<Venta>> Get(int id)
        {
            var item = await _context.Ventas.FindAsync(id);
            return item == null ? NotFound() : item;
        }

        [HttpPost]
        public async Task<ActionResult<Venta>> Post(Venta item)
        {
            _context.Ventas.Add(item);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = item.VentaId }, item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Venta item)
        {
            if (id != item.VentaId) return BadRequest();
            _context.Entry(item).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Ventas.AnyAsync(e => e.VentaId == id)) return NotFound();
                throw;
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.Ventas.FindAsync(id);
            if (item == null) return NotFound();
            _context.Ventas.Remove(item);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
