using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pdtcc_doc_academy.Models
{
    public class AtestadoMatricula
    {
        [Key]
        public int IdAtestMat { get; set; }
        [ForeignKey("doc")]
        private int fkDoc { get; set; }
    }
}
