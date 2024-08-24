using pdtcc_doc_academy.Models;

namespace pdtcc_doc_academy.Repositories
{
    public interface IAutorizacaoRepository
    {
        Task<List<Autorizacao>> Getall();
        Task<Autorizacao> GetById(int id);
        Task Add(Autorizacao autorizacao);
        Task Update(Autorizacao autorizacao);
        Task Delete(Autorizacao autorizacao);
    }
}
