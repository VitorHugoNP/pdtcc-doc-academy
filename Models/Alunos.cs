using System.ComponentModel.DataAnnotations;

namespace pdtcc_doc_academy.Models
{
    public class Alunos
    {
        [Key]
        public int idAluno { get; set; }

        [Required(ErrorMessage = "Por favor, informe o nome completo do aluno.")]
        [Display(Name = "Nome Completo")]
        public string nomeAluno { get; set; }

        [Required(ErrorMessage = "Por favor, informe o CPF do aluno.")]
        [Display(Name = "CPF")]
        public int cpfAluno { get; set; }

        [Required(ErrorMessage = "Por favor, informe o CPF do aluno.")]
        [Display(Name = "RG")]
        public int rgAluno { get; set; }

        [Display(Name = "RM")]
        public int rmAluno { get; set; }

        [Required(ErrorMessage = "Por favor, informe o CPF do aluno.")]
        [Display(Name = "EMAIL")]
        public string emailAluno { get; set; }

        [Required(ErrorMessage = "Por favor, informe a senha do aluno.")]
        [Display(Name = "Senha")]
        public string senhaAluno { get; set; }

        public ICollection<AlunoSerie> AlunoSeries { get; set; }
        public ICollection<AlunoSerie> AlunoCursos { get; set; }

        public ICollection<Protocolo> Protocolos { get; set; }


    }
}