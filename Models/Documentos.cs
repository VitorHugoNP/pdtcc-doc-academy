using System.ComponentModel.DataAnnotations;

namespace pdtcc_doc_academy.Models
{
    public class Documentos
    {
        [Key]
        public int doc { get; set; }

        [Required]
        private string modelDocumento { get; set; }
    }
}
