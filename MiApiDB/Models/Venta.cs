using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiApiDB.Models
{
    [Table("ventas")]
    public class Venta
    {
        [Key]
        [Column("venta_id")]
        public int VentaId { get; set; }

        [Column("usuario_id")]
        public int UsuarioId { get; set; }

        [Column("producto_id")]
        public int ProductoId { get; set; }

        [Column("fecha")]
        public DateTime Fecha { get; set; }

        [Column("monto")]
        public decimal Monto { get; set; }

        [Column("metodo_pago")]
        public byte MetodoPago { get; set; }

        [ForeignKey("ProductoId")]
        public Producto Producto { get; set; } = null!;
    }
}
