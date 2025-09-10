using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiApiDB.Models
{
    [Table("categorias_insumo")]
    public class CategoriaInsumo
    {
        [Key]
        [Column("categoria_id")]
        public int CategoriaId { get; set; }

        [Column("nombre")]
        [Required]
        public string Nombre { get; set; }
    }
}
