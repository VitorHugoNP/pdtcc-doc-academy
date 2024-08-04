using System.ComponentModel.DataAnnotations;

namespace pdtcc_doc_academy.Models
{
    public class Materia
    {
        [Key]
        public int IdMateria { get; set; }
        [Required]
        public string nomeMateria { get; set; }
    }
}
