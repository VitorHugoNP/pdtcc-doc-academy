using pdtcc_doc_academy.Models;
using System.ComponentModel.DataAnnotations;



namespace pdtcc_doc_academy.Models
{ 
    public class Protocolo
    {
        [Key]
        public int idProtocolo { get; set; }
        public int fk_aluno { get; set; }
        public int fk_func { get; set; }

        public string tipo_Doc { get; set; }

        // Propriedades de navegação (não são chaves estrangeiras diretas)
        public Alunos Aluno { get; set; }
        public Funcionarios Funcionario { get; set; }

        public Usuario Usuario { get; set; }
    }
}