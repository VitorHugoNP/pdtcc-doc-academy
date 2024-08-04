using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pdtcc_doc_academy.Models
{
    public class GradeCurricular
    {
        [Key]
        public int IdGradeCurricular { get; set; }
        [ForeignKey("doc")]
        private int fkDoc { get; set; }


    }
}
