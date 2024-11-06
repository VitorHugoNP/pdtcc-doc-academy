using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pdtcc_doc_academy.Models
{
    public class Comunicados
    {
        [Key]
        public int idComunicados { get; set; }
        [Required]
        public DateTime data_comunicado { get; set; }
        [ForeignKey("fk_prot")]
        public int fk_prot {  get; set; }
        public Protocolo Protocolo { get; set; }
    }
}
