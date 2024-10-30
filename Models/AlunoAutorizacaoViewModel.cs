using System.ComponentModel.DataAnnotations;

namespace pdtcc_doc_academy.Models
{
    public class AlunoAutorizacaoViewModel
    {
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

        public int fk_usuario { get; set; }

        public Usuario usuario { get; set; }

        public ICollection<AlunoCurso> alunoCursos { get; set; }
        public ICollection<AlunoSerie> alunoSeries { get; set; }
        public ICollection<Protocolo> protocolos { get; set; }


        public List<Alunos> Alunos { get; set; }
        public List<Autorizacao> Autorizacao { get; set; }
    }
}
