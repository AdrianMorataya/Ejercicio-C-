using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiApiDB.Models
{
    [Table("usuarios")]
    public class Usuario
    {
        [Key]
        [Column("usuario_id")]
        public int UsuarioId { get; set; }

        [Column("nombre")]
        [Required]
        public string Nombre { get; set; }

        [Column("apellido")]
        [Required]
        public string Apellido { get; set; }

        [Column("correo")]
        [Required]
        public string Correo { get; set; }

        [Column("password_hash")]
        [Required]
        public string PasswordHash { get; set; }

        [NotMapped]
        public bool IsAdmin { get; set; }

        [Column("rol")]
        [Required]
        public string Rol { get; set; }
    }
}
