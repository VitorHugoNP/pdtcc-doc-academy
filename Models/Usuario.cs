using System.ComponentModel.DataAnnotations;

namespace pdtcc_doc_academy.Models
{
    public class Usuario
    {
        [Key]
        public int idUsuario { get; set; }

        [Required]
        [EmailAddress]
        public string emailUsuario { get; set; }

        [Required]
        public string senhaUsuario { get; set; }

        [Required]
        public string tipoUsuario { get; set; }
    }
}
