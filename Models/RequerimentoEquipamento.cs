using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pdtcc_doc_academy.Models
{
    public class RequerimentoEquipamento
    {
        [Key]
        public int IdReq { get; set; }
        [ForeignKey("doc")]
        [Required]
        private int fkDoc { get; set; }
        [Required]
        public DateTime dataReq { get; set; }
    }
}
