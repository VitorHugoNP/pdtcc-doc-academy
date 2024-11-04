using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pdtcc_doc_academy.Models
{
    public class Atestado_Matricula
    {
        [Key]
        public int IdAtest_mat { get; set; }
        [ForeignKey("fk_prot")]
        public int fk_prot { get; set; }
        public Protocolo Protocolo { get; set; }
    }
}
