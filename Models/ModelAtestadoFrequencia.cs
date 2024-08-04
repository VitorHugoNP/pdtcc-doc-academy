using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace pdtcc_doc_academy.Models
{
    public class ModelAtestadoFrequencia
    {
        [Required]
        public int IdFrequencia { get; set; }
        [ForeignKey ("IdDoc")]
        private int fkDoc { get; set; }
    }
}
