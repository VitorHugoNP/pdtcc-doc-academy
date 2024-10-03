using System.ComponentModel.DataAnnotations;

namespace pdtcc_doc_academy.Models
{
    public class Usuario
    {
        [Key]
        public int idUsuario { get; set; }
        public string nome_Usuario { get; set; }
        public string email_Usuario { get; set; }
        public string senha_Usuario { get; set; }
 
    }
}
