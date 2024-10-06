using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pdtcc_doc_academy.Models
{
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

        public ICollection<Funcionario> funcionarios { get; set; }

    }
}
