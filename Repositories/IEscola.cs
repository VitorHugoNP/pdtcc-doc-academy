using pdtcc_doc_academy.Models;

namespace pdtcc_doc_academy.Repositories
{
    public interface IEscola
    {
        IEnumerable<Escola> Getall();
        IEnumerable<Escola> GetById(int id);
        void Add(Escola escola);
        void Update(Escola escola);
        void Delete(int id);
    }
}
