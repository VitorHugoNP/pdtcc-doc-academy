using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pdtcc_doc_academy.Models
{
    public class Alunos
    {
        [Key]
        public int idAluno { get; set; }

        [Required]
        [MaxLength(100)]
        public string nomeAluno { get; set; }

        [Required]
        public int cpfAluno { get; set; }

        [Required]
        public int rgAluno { get; set; }

        [Required]
        public int rmAluno { get; set; }

        [Required]
        [MaxLength(150)]
        public string emailAluno { get; set; }

        [Required]
        [MaxLength(100)]
        public string senhaAluno { get; set; }

        [ForeignKey("fk_usuario")]
        public int fk_usuario { get; set; }

        public Usuario usuario { get; set; }
        // Relacionamento com AlunoCurso e AlunoSerie
        public ICollection<AlunoCurso> alunoCursos { get; set; }
        public ICollection<AlunoSerie> alunoSeries { get; set; }
        // Relacionamento com Protocolos
        public ICollection<Protocolo> protocolos { get; set; }
    }
}