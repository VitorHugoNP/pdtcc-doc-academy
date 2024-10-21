using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace pdtcc_doc_academy.Models
{
    [Table("escola")]
    public class Escola
    {
        [Key]
        public int idEscola { get; set; }

        [Required]
        [MaxLength(70)]
        public string nomeEscola { get; set; }

        [Required]
        [MaxLength(200)]
        public string enderecoEscola { get; set; }

        [Required]
        [MaxLength(150)]
        public string emailEscola { get; set; }

        [Required]
        [MaxLength(100)]
        public string senhaEscola { get; set; }

        [ForeignKey("fk_usuario")]
        public int fk_usuario { get; set; }

        public Usuario usuario { get; set; }

        public ICollection<Funcionario> funcionarios { get; set; }

    }
}
