using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiApiDB.Models
{
    [Table("productos")]
    public class Producto
    {
        [Key]
        [Column("producto_id")]
        public int ProductoId { get; set; }

        [Column("tipo_id")]
        public int TipoId { get; set; }

        [Column("stock")]
        public int Stock { get; set; }

        [Column("nombre")]
        [Required]
        public string Nombre { get; set; }

        [Column("precio")]
        public decimal Precio { get; set; }

        [Column("activo")]
        public bool Activo { get; set; } = true;
    }
}
