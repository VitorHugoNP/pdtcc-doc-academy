
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
        public string nome_Func { get; set; }

        [Required(ErrorMessage = "Por favor, informe o email do funcionário.")]
        [Display(Name = "Email")]
        public string email_Func { get; set; }

        [Required(ErrorMessage = "Por favor, informe a senha do funcionário.")]
        [Display(Name = "Senha")]
        public string senha_Func { get; set; }

        //public ICollection<Escolas> Escolas { get; set; }

        // Propriedade de navegação
        public ICollection<Protocolo> Protocolos { get; set; }
    }
}