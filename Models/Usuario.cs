using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pdtcc_doc_academy.Models
{
    [Table("usuario")]
    public class Usuario
    {
        [Key]
        public int idUsuario { get; set; }

        [Required]
        [MaxLength(150)]
        public string emailUsuario { get; set; }

        [Required]
        [MaxLength(100)]
        public string senhaUsuario { get; set; }

        [Required]
        [MaxLength(100)]
        public string tipoUsuario { get; set; }

        public ICollection<Alunos> alunos { get; set; }
        public ICollection<Escola> escolas  { get; set; }
    }
}
