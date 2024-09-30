using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pdtcc_doc_academy.Models
{
    public class Curso
    {
        [Key]
        public int IdCurso { get; set; }

        [Required(ErrorMessage = "Por favor, informe o nome do curso.")]
        [Display(Name = "Nome do Curso")]
        public string NomeCurso { get; set; }

        // Propriedade de navegação
        public ICollection<AlunoCurso> AlunoCursos { get; set; }
    }
}