
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography.X509Certificates;

namespace pdtcc_doc_academy.Models
{
    public class Funcionario
    {
        [Key]
        public int IdFuncionario { get; set; }

        [Required(ErrorMessage = "Por favor, informe o nome do funcionário.")]
        [Display(Name = "Nome")]
        public string nome_func { get; set; }

        [Required(ErrorMessage = "Por favor, informe o email do funcionário.")]
        [Display(Name = "Email")]
        public string email_func { get; set; }

        [Required(ErrorMessage = "Por favor, informe a senha do funcionário.")]
        [Display(Name = "Senha")]
        public string senha_func { get; set; }

        public int fk_escola { get; set; }

        public Escolas Escolas { get; set; }

        public ICollection<Protocolo> Protocolos { get; set; }

        //public ICollection<Escolas> Escolas { get; set; }

        // Propriedade de navegação

        //public ICollection<Usuario> usuarios { get; set; }

    }
}