using pdtcc_doc_academy.Models;

namespace pdtcc_doc_academy.Repositories
{
    public interface IEscolaRepository
    {
        Task<List<Escolas>> Getall();
        Task<Escolas> GetById(int id);
        Task Add(Escolas escola);
        Task Update(Escolas escola);
        Task Delete(Escolas escola);
    }
}
