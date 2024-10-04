using System.ComponentModel.DataAnnotations;

namespace pdtcc_doc_academy.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Senha { get; set; }

        [Required]
        public string TipoUsuario { get; set; }
    }
}
