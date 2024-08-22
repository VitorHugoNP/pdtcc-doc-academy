using System.ComponentModel.DataAnnotations;

namespace pdtcc_doc_academy.Models
{
    public class Aluno
    {
        [Key]
        public int IdAluno { get; set; }
        [Required]
        public string nomeAluno { get; set; }
        [Required]
        public int cpfAluno { get; set; }
        [Required]
        public string cursoAluno { get; set; }
        [Required]
        public int rmAluno { get; set; }
        [Required]
        public string senhaAluno { get; set; }

    }
}
