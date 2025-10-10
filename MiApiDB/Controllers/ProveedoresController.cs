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
            await _context.Proveedores.Where(p => p.Activo).ToListAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<Proveedor>> Get(int id)
        {
            var item = await _context.Proveedores.FindAsync(id);
            return item == null || !item.Activo ? NotFound() : item;
        }

        [HttpPost]
        public async Task<ActionResult<Proveedor>> Post(ProveedorCrearDTO dto)
        {
            var item = new Proveedor
            {
                Nombre = dto.Nombre,
                Contacto = dto.Contacto,
                Telefono = dto.Telefono,
                Direccion = dto.Direccion,
                Activo = true
            };

            _context.Proveedores.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = item.ProveedorId }, item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, ProveedorCrearDTO dto)
        {
            var existing = await _context.Proveedores.FindAsync(id);
            if (existing == null || !existing.Activo) return NotFound();

            existing.Nombre = dto.Nombre;
            existing.Contacto = dto.Contacto;
            existing.Telefono = dto.Telefono;
            existing.Direccion = dto.Direccion;

            _context.Entry(existing).State = EntityState.Modified;

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

            // Eliminación lógica
            item.Activo = false;
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
