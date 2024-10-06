using System.ComponentModel.DataAnnotations;

namespace pdtcc_doc_academy.Models
{
    public class Usuario
    {
        [Key]
        public int idUsuario { get; set; }

        [Required]
        [MaxLength(150)]
        public string emailUsuario { get; set; }

        [Required]
        [MaxLength(100)]
        public string senhaUsuario { get; set; }

        [Required]
        [MaxLength(100)]
        public string tipoUsuario { get; set; }
    }
}
