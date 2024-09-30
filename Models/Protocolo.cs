using pdtcc_doc_academy.Models;



namespace pdtcc_doc_academy.Models
{ 
    public class Protocolo
    {
        public int Id { get; set; }
        public int Id_Aluno { get; set; }
        public int Id_Funcionario { get; set; }

        // Propriedades de navegação (não são chaves estrangeiras diretas)
        public Alunos Aluno { get; set; }
        public Funcionarios Funcionario { get; set; }
    }
}