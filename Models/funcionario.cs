using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pdtcc_doc_academy.Models
{
    public class Funcionario
    {
        [Key]
        public int idFunc { get; set; }
        [Required]
        public string nomeFunc { get; set; }
        [Required]
        [EmailAddress]
        public string emailFunc { get; set; }
        [Required]
        public string senhaFunc { get; set; }
        [ForeignKey("Cargo")]
        private int fkCargo { get; set; }
        [ForeignKey("Escola")]
        private int fkEscola { get; set; }
    }
}
