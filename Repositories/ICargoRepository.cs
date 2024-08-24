using pdtcc_doc_academy.Models;

namespace pdtcc_doc_academy.Repositories
{
    public interface ICargoRepository
    {
        Task<List<Cargo>> Getall();
        Task<Cargo> GetById(int id);
        Task Add(Cargo cargo);
        Task Update(Cargo cargo);
        Task Delete(Cargo cargo);
    }
}
