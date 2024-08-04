using System.ComponentModel.DataAnnotations.Schema;

namespace pdtcc_doc_academy.Models
{
    public class Autorizacao
    {
        public int IdAutorizacao { get; set; }
        public DateTime? dataAut { get; set; }
        [ForeignKey("doc")]
        public int fkDoc { get; set; }
    }
}
