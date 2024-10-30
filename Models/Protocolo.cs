using pdtcc_doc_academy.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;


namespace pdtcc_doc_academy.Models
{ 
    public class Protocolo
    {
        [Key]
        public int idProtocolo { get; set; }
        //[ForeignKey("aluno")]
        public int? fk_aluno { get; set; }
        //[ForeignKey("funcionario")]
        public int? fk_func { get; set; }
        public string tipo_Doc { get; set; }
        // Propriedades de navegação (não são chaves estrangeiras diretas)
        public Alunos aluno { get; set; }
        public Funcionario funcionario { get; set; }


        public ICollection<Autorizacao> autorizacaos { get; set; }

    }
}