using pdtcc_doc_academy.Models;
using System.ComponentModel.DataAnnotations;



namespace pdtcc_doc_academy.Models
{ 
    public class Protocolo
    {
        [Key]
        public int Id { get; set; }
        public int idAluno { get; set; }
        public int idFuncionario { get; set; }

        public string tipo_Doc { get; set; }

        // Propriedades de navegação (não são chaves estrangeiras diretas)
        public Alunos Aluno { get; set; }
        public Funcionarios Funcionario { get; set; }
    }
}