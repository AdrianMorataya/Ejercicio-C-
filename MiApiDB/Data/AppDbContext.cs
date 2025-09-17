using Microsoft.EntityFrameworkCore;
using MiApiDB.Models;

namespace MiApiDB.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<TipoProducto> TiposProducto { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Venta> Ventas { get; set; }
        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<CategoriaInsumo> CategoriasInsumo { get; set; }
        public DbSet<Insumo> Insumos { get; set; }
        public DbSet<ProductoInsumo> ProductosInsumos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductoInsumo>()
                .HasKey(pi => new { pi.ProductoId, pi.InsumoId });
        }
    }
}
