using pdtcc_doc_academy.Models;

namespace pdtcc_doc_academy.Repositories
{
    public interface IDocumentosRepository
    {
        IEnumerable<Documentos> Getall();
        IEnumerable<Documentos> GetById(int id);
        void Add(Documentos documentos);
        void Update(Documentos documentos);
        void Delete(int id);
    }
}
