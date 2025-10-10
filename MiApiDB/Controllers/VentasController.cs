using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiApiDB.Data;
using MiApiDB.Models;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using System.IO;
using System.Globalization;

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


        [HttpGet("reporte")]
        public async Task<ActionResult<ReporteVentas>> GetReporteVentas()
        {
            var ventas = await _context.Ventas.ToListAsync();
            var totalVentas = ventas.Sum(v => v.Monto);

            var totalInsumos = _context.Insumos.Sum(i => i.Costo);
            var totalProductos = _context.Productos.Sum(p => p.Precio * p.Stock);

            var reporte = new ReporteVentas
            {
                Ventas = ventas,
                TotalVentas = totalVentas,
                TotalInsumos = totalInsumos,
                TotalProductos = totalProductos
            };

            return Ok(reporte);
        }

        [HttpGet("reporte/pdf")]
        public async Task<IActionResult> GetReporteVentasPDF()
        {
            var ventas = await _context.Ventas
                .Include(v => v.Producto)
                .ToListAsync();

            var totalVentas = ventas.Sum(v => v.Monto);

            decimal totalInsumos = _context.Insumos.Sum(i => i.Costo);
            decimal totalProductos = 0;

            foreach (var v in ventas)
            {
                var producto = await _context.Productos.FindAsync(v.ProductoId);
                if (producto != null)
                {
                    totalProductos = _context.Productos.Sum(p => p.Precio * p.Stock);
                }
            }

            var ganancia = totalVentas - totalInsumos;
            var culture = new CultureInfo("es-GT");

            using var ms = new MemoryStream();
            var writer = new PdfWriter(ms);
            var pdf = new PdfDocument(writer);
            var document = new Document(pdf);

            document.Add(new Paragraph("Reporte de Ventas").SetFontSize(18));
            document.Add(new Paragraph($"Total Ventas: {totalVentas:C}"));
            document.Add(new Paragraph($"Total Insumos: {totalInsumos:C}"));
            document.Add(new Paragraph($"Total Productos: {totalProductos:C}"));
            document.Add(new Paragraph($"Ganancia: {ganancia.ToString("C", culture)}"));

            document.Add(new Paragraph("Detalle de ventas:").SetFontSize(14));
            foreach (var v in ventas)
            {
                document.Add(new Paragraph($"VentaId: {v.VentaId} - Monto: {v.Monto:C} - Fecha: {v.Fecha:d}"));
            }

            document.Close();

            var bytes = ms.ToArray();
            return File(bytes, "application/pdf", "ReporteVentas.pdf");
        }
    }
}
