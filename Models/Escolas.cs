using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pdtcc_doc_academy.Models
{
    public class Escolas
    {
        [Key]
        public int idEscola { get; set; }
        [Required]
        public required string nomeEscola { get; set; }
        [Required]
        public required string enderecoEscola { get; set; }
        [Required]
        [EmailAddress]
        public required string emailEscola { get; set; }
        [Required]
        public string senhaEscola { get; set; }

        public Usuario Usuario { get; set; }

        //public ICollection<Usuario> Usuario { get; set; }

    }
}
