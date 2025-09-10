using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiApiDB.Models
{
    [Table("insumos")]
    public class Insumo
    {
        [Key]
        [Column("insumo_id")]
        public int InsumoId { get; set; }

        [Column("categoria_id")]
        public int CategoriaId { get; set; }

        [Column("proveedor_id")]
        public int ProveedorId { get; set; }

        [Column("nombre")]
        [Required]
        public string Nombre { get; set; }

        [Column("costo")]
        public decimal Costo { get; set; }
    }
}
