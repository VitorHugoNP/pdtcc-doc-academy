
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography.X509Certificates;

namespace pdtcc_doc_academy.Models
{
    public class Funcionario
    {
        [Key]
        [Display(Name = "Id do Funcionario")]
        public int idFuncionario { get; set; }

        [Required]
        [MaxLength(45)]
        [Display(Name = "Nome do Funcionario")]
        public string nome_func { get; set; }

        [Required]
        [MaxLength(150)]
        [Display(Name = "Email do Funcionario")]
        public string email_func { get; set; }

        [Required]
        [MaxLength(150)]
        [Display(Name = "Senha do Funcionario")]
        public string senha_func { get; set; }
        
        [ForeignKey("fk_escola")]
        [Display(Name = "Id da Escola")]
        public int? fk_escola { get; set; }
        
        [ForeignKey("fk_usuario")]
        public int? fk_usuario { get; set; }

        public Escola escola { get; set; }
        public Usuario usuario { get; set; }
        public ICollection<Protocolo> Protocolos { get; set; }

    }
}