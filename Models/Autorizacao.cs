using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pdtcc_doc_academy.Models
{
    public class Autorizacao
    {
        [Key]
        public int idAutorizacao { get; set; }
        public DateTime? data_aut { get; set; }
        [ForeignKey("fk_prot")]
        public int fk_prot { get; set; }
        public Protocolo Protocolo { get; set; }
    }
}
