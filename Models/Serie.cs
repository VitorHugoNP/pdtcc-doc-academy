using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pdtcc_doc_academy.Models
{
    public class Serie
    {
        [Key]
        public int idSerie { get; set; }

        [Required(ErrorMessage = "Por favor, informe o nome da série.")]
        [Display(Name = "Nome da Série")]
        public string NomeSerie { get; set; }

        public int IdAluno { get; set; }

        public ICollection<AlunoSerie> AlunoSeries { get; set; }
        
    }
}