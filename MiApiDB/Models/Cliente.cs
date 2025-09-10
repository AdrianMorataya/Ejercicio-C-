using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiApiDB.Models
{
    [Table("clientes")]
    public class Cliente
    {
        [Key]
        [Column("cliente_id")]
        public int ClienteId { get; set; }

        [Column("nombre")]
        [Required]
        public string Nombre { get; set; }

        [Column("correo_contacto")]
        [Required]
        public string CorreoContacto { get; set; }

        [Column("direccion")]
        public string Direccion { get; set; }

        [Column("zona")]
        public int Zona { get; set; }
    }
}
