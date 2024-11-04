using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pdtcc_doc_academy.Models
{
    public class AtestadoMatricula
    {
        [Key]
        public int IdAtest_mat { get; set; }
        [ForeignKey("fk_prot")]
        public int fk_prot { get; set; }
        public virtual Protocolo Protocolo { get; set; }
    }
}
