using System.ComponentModel.DataAnnotations;

namespace pdtcc_doc_academy.Models
{
    //classe principal
    public class Documentos
    {
        [Key]
        public int IdDoc { get; set; }

        [Required]
        private string modelDocumento { get; set; }
    }
}
