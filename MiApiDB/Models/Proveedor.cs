using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiApiDB.Models
{
    [Table("proveedores")]
    public class Proveedor
    {
        [Key]
        [Column("proveedor_id")]
        public int ProveedorId { get; set; }

        [Column("nombre")]
        [Required]
        public string Nombre { get; set; }

        [Column("contacto")]
        public string Contacto { get; set; }

        [Column("telefono")]
        public string Telefono { get; set; }

        [Column("direccion")]
        public string Direccion { get; set; }
    }
}
