using pdtcc_doc_academy.Models;

namespace pdtcc_doc_academy.Repositories
{
    public interface IAutorizacao
    {
        IEnumerable<Autorizacao> Getall();
        IEnumerable<Autorizacao> GetById(int id);
        void Add(Autorizacao autorizacao);
        void Update(Autorizacao autorizacao);
        void Delete(int id);
    }
}
