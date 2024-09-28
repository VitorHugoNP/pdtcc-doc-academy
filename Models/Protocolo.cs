using pdtcc_doc_academy.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SeuProjeto
{
    public class Protocolo
    {
        [Key]
        public int ProtocoloId { get; set; }
        // Outros atributos do protocolo...
        public ICollection<Prof_Materias> ProfMaterias { get; set; }
    }




}
