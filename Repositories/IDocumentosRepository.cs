using pdtcc_doc_academy.Models;

namespace pdtcc_doc_academy.Repositories
{
    public interface IDocumentosRepository
    {
        Task<List<Documentos>> Getall();
        Task<Documentos> GetById(int id);
        Task Add(Documentos documentos);
        Task Update(Documentos documentos);
        Task Delete(Documentos documentos);
    }
}
