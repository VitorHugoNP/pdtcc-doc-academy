namespace pdtcc_doc_academy.Models
{
    public class AlunoComunicado
    {
        public int idAluno { get; set; } // Renomeado para idAluno

        public string nomeAluno { get; set; }
        public int cpfAluno { get; set; }
        public int rgAluno { get; set; }
        public int rmAluno { get; set; }

        public int idComunicados { get; set; }
        public DateTime data_comunicado { get; set; }
    }
}
