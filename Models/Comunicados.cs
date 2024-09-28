using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pdtcc_doc_academy.Models
{
    public enum StatusComunicado
    {
        Solicitado,
        EmAndamento,
        Finalizado
    }

    public class Comunicados
    {
        [Key]
        public int idComunicados { get; set; }
        [Required]
        public int descricaoComunicados { get; set; }
        [Required]
        public DateTime dataComunicado { get; set; }
        [ForeignKey("doc")]
        public int fkDoc { get; set; }
    }
}
