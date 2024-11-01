using System.ComponentModel.DataAnnotations;

namespace pdtcc_doc_academy.Models
{
    public class AlunoAutorizacao
    {
        public int idAluno { get; set; }
        public string nomeAluno { get; set; }
        public int cpfAluno { get; set; }
        public int rgAluno { get; set; }
        public int rmAluno { get; set; }

        // Atributos da classe Autorizacao
        public int idAutorizacao { get; set; }
        public DateTime? data_aut { get; set; }
        public int fk_prot { get; set; }
    }
}
