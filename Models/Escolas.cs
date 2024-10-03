using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pdtcc_doc_academy.Models
{
    public class Escolas
    {
        [Key]
        [Column("idEscola")]
        public int IdEscola { get; set; }
        [Required]
        [Column("nomeEscola")]
        public required string nomeEscola { get; set; }
        [Required]
        [Column("enderecoEscola")]
        public required string enderecoEscola { get; set; }
        [Required]
        [EmailAddress]
        [Column("emailEscola")]
        public required string emailEscola { get; set; }
        [Column("senhaEscola")]
        public string senhaEscola { get; set; }

        public ICollection<Usuario> Usuario { get; set; }

    }
}
