using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pdtcc_doc_academy.Models
{
    public class Funcionarios
    {
        [Key]
        [Column("idFuncionario")]
        public int idFuncionario { get; set; }
        [Column("nome_func")]
        [Required]
        public required string NomeFuncionario { get; set; }
        [Required]
        [EmailAddress]
        [Column("email_func")]
        public required string EmailFuncionario { get; set; }
        [Required]
        [Column("senha_func")]
        public required string SenhaFuncionario { get; set; }
        [ForeignKey("Escola")]
        private int fk_escola { get; set; }
        [ForeignKey("Cargo")]
        private int fk_cargo { get; set; }

    }
}
