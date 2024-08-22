using System.ComponentModel.DataAnnotations;

namespace pdtcc_doc_academy.Models
{
    public class Escolas
    {
        [Key]
        public int IdEscola { get; set; }
        [Required]
        public string nomeEscola { get; set; }
        [Required]
        public string enderecoEscola { get; set; }
        [Required]
        [EmailAddress]
        public string emailEscola { get; set; }
        [Required]
        public int telefoneEscola { get; set; }
    }
}
