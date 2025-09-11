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
    public class InsumosController : ControllerBase
    {
        private readonly AppDbContext _context;
        public InsumosController(AppDbContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Insumo>>> Get() =>
            await _context.Insumos.ToListAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<Insumo>> Get(int id)
        {
            var item = await _context.Insumos.FindAsync(id);
            return item == null ? NotFound() : item;
        }

        [HttpPost]
        public async Task<ActionResult<Insumo>> Post(Insumo item)
        {
            _context.Insumos.Add(item);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = item.InsumoId }, item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Insumo item)
        {
            if (id != item.InsumoId) return BadRequest();
            _context.Entry(item).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Insumos.AnyAsync(e => e.InsumoId == id)) return NotFound();
                throw;
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.Insumos.FindAsync(id);
            if (item == null) return NotFound();
            _context.Insumos.Remove(item);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
