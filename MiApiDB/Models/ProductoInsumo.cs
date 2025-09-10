using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiApiDB.Models
{
    [Table("productos_insumos")]
    public class ProductoInsumo
    {
        public int ProductoId { get; set; }

        public int InsumoId { get; set; }
        public decimal Cantidad { get; set; }
    }
}
