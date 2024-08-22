using pdtcc_doc_academy.Models;

namespace pdtcc_doc_academy.Repositories
{
    public interface IEscolaRepository
    {
        IEnumerable<Escolas> Getall();
        IEnumerable<Escolas> GetById(int id);
        void Add(Escolas escola);
        void Update(Escolas escola);
        void Delete(int id);
    }
}
