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
    public class CategoriasInsumoController : ControllerBase
    {
        private readonly AppDbContext _context;
        public CategoriasInsumoController(AppDbContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoriaInsumo>>> Get() =>
            await _context.CategoriasInsumo.ToListAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoriaInsumo>> Get(int id)
        {
            var item = await _context.CategoriasInsumo.FindAsync(id);
            return item == null ? NotFound() : item;
        }

        [HttpPost]
        public async Task<ActionResult<CategoriaInsumo>> Post(CategoriaInsumo item)
        {
            _context.CategoriasInsumo.Add(item);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = item.CategoriaId }, item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, CategoriaInsumo item)
        {
            if (id != item.CategoriaId) return BadRequest();
            _context.Entry(item).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.CategoriasInsumo.AnyAsync(e => e.CategoriaId == id)) return NotFound();
                throw;
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.CategoriasInsumo.FindAsync(id);
            if (item == null) return NotFound();
            _context.CategoriasInsumo.Remove(item);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
