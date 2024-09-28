using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace pdtcc_doc_academy.Models
{
    public class Prof_Materias
    {
        [Key]
        public int id { get; set; }

        [ForeignKey("Professor")]
        public int ProfessorId { get; set; }
        [ForeignKey("Aluno")]
        public int MateriaId { get; set; }

        public virtual Professores Professor { get; set; }
        public virtual Alunos Aluno { get; set; }
    }
}
