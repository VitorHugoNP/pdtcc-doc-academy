using System.ComponentModel.DataAnnotations;

namespace pdtcc_doc_academy.Models
{
    public class Cargo
    {
        [Key]
        public int IdCargo { get; set; }
        [Required]
        public string nomeCargo { get; set; }
    }
}
