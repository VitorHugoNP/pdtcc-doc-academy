using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pdtcc_doc_academy.Models
{
    public class AlunoCurso
    {
        [Required(ErrorMessage = "Por favor, informe o ID do aluno.")]
        [Display(Name = "ID do Aluno")]
        [Key]
        public int IdAluno { get; set; }

        [Required(ErrorMessage = "Por favor, informe o ID do curso.")]
        [Display(Name = "ID do Curso")]
        public int IdCurso { get; set; }

        // Propriedades de navegação
        public Alunos Aluno { get; set; }
        public Curso Curso { get; set; }
    }
}