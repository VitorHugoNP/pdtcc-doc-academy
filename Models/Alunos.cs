using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pdtcc_doc_academy.Models
{
    public class Alunos
    {
        [Key]
        [Display(Name = "Id do Aluno")]
        public int idAluno { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "Nome do Aluno")]
        public string nomeAluno { get; set; }

        [Required]
        [Display(Name = "Cpf do Aluno")]
        public int cpfAluno { get; set; }

        [Required]
        [Display(Name = "RG do aluno")]
        public int rgAluno { get; set; }

        [Required]
        [Display(Name = "RM do Aluno")]
        public int rmAluno { get; set; }

        [Required]
        [MaxLength(150)]
        [Display(Name = "Email do Aluno")]
        public string emailAluno { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "Senha do Aluno")]
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