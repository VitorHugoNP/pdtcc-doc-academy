﻿using pdtcc_doc_academy.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SeuProjeto
{
    public class Protocolo
    {
        [Key]
        public int ProtocoloId { get; set; }
        // Outros atributos do protocolo...
        public ICollection<Prof_Materia> ProfMaterias { get; set; }
    }

    public class Prof_Materia
    {
        [ForeignKey("Professor")]
        public int ProfessorId { get; set; }
        [ForeignKey("Materia")]
        public int MateriaId { get; set; }

        public virtual Professor Professor { get; set; }
        public virtual Materia Materia { get; set; }
    }

    public class Professor
    {
        [Key]
        public int IdProfessor { get; set; }
        public string NomeProfessor { get; set; }
        public int CpfProfessor { get; set; }
        public string SenhaProf { get; set; }

        public ICollection<Prof_Materia> ProfMaterias { get; set; }
    }
}