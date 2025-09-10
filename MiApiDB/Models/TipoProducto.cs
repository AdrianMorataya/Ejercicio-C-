using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiApiDB.Models
{
    [Table("tipos_producto")]
    public class TipoProducto
    {
        [Key]
        [Column("tipo_id")]
        public int TipoId { get; set; }

        [Column("nombre")]
        [Required]
        public string Nombre { get; set; }
    }
}
