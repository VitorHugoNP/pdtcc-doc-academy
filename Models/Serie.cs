using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pdtcc_doc_academy.Models
{
    public class Serie
    {
        [Key]
        public int IdSerie { get; set; }

        [Required(ErrorMessage = "Por favor, informe o nome da série.")]
        [Display(Name = "Nome da Série")]
        public string NomeSerie { get; set; }

        // Propriedade de navegação
        public ICollection<AlunoSerie> AlunoSeries { get; set; }
    }
}