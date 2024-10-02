using pdtcc_doc_academy.Models;

namespace pdtcc_doc_academy.Repositories
{
    public interface IAutorizacaoRepository
    {
        Task Add(Autorizacao autorizacao);
        Task Delete(Autorizacao autorizacao);
        Task<List<Autorizacao>> GetAll();
        Task<Autorizacao> GetById(int id);
        Task Update(Autorizacao autorizacao);
    }
}