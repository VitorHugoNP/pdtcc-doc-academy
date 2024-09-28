using SeuProjeto;
using System.ComponentModel.DataAnnotations;

namespace pdtcc_doc_academy.Models
{
    public class Professores
    {
        [Key]
        public int IdProfessor { get; set; }
        public string NomeProfessor { get; set; }
        public int CpfProfessor { get; set; }
        public string SenhaProf { get; set; }

        public ICollection<Prof_Materias> ProfMaterias { get; set; }
    }
}
