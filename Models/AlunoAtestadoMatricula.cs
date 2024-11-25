using pdtcc_doc_academy.Models;

namespace pdtcc_doc_academy.Models
{
    
    public class AlunoAtestadoMatricula
    {
        public int idAluno { get; set; } // Renomeado para idAluno
        public string nomeAluno { get; set; }
        public int cpfAluno { get; set; }
        public int rgAluno { get; set; }
        public int rmAluno { get; set; }
        public string serieAluno { get; set; }
        public string cursoAluno { get; set; }
        public int IdAtest_mat { get; set; }
    }
}
