
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pdtcc_doc_academy.Models
{
    public class Funcionarios
    {
        [Key]
        public int IdFuncionario { get; set; }

        [Required(ErrorMessage = "Por favor, informe o nome do funcionário.")]
        [Display(Name = "Nome")]
        public string NomeFuncionario { get; set; }

        [Required(ErrorMessage = "Por favor, informe o CPF do funcionário.")]
        [Display(Name = "CPF")]
        public int CpfFuncionario { get; set; }

        [Required(ErrorMessage = "Por favor, informe o email do funcionário.")]
        [Display(Name = "Email")]
        public string EmailFuncionario { get; set; }

        [Required(ErrorMessage = "Por favor, informe a senha do funcionário.")]
        [Display(Name = "Senha")]
        public string SenhaFuncionario { get; set; }

        // Propriedade de navegação
        public ICollection<Protocolo> Protocolos { get; set; }
    }
}