using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pdtcc_doc_academy.Models
{
    public class Protocolo
    {
        [Key]
        public int IdProtocolo { get; set; }

        [Required(ErrorMessage = "Por favor, informe o ID do aluno.")]
        [Display(Name = "ID do Aluno")]
        public int IdAluno { get; set; }

        [Required(ErrorMessage = "Por favor, informe o ID do funcionário.")]
        [Display(Name = "ID do Funcionário")]
        public int IdFuncionario { get; set; }

        [Required(ErrorMessage = "Por favor, informe a data do protocolo.")]
        [Display(Name = "Data do Protocolo")]
        public DateTime DataProtocolo { get; set; }


        // Propriedades de navegação
        public Alunos Aluno { get; set; }
        public Funcionarios Funcionario { get; set; }
    }
}