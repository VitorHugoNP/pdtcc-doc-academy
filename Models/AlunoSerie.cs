using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pdtcc_doc_academy.Models
{
    public class AlunoSerie
    {
        [Key]
        public int IdAlunoSerie { get; set; }

        [Required(ErrorMessage = "Por favor, informe o ID do aluno.")]
        [Display(Name = "ID do Aluno")]
        public int IdAluno { get; set; }

        [Required(ErrorMessage = "Por favor, informe o ID da série.")]
        [Display(Name = "ID da Série")]
        public int IdSerie { get; set; }

        // Propriedades de navegação
        public Alunos Aluno { get; set; }
        public Serie Serie { get; set; }
    }
}