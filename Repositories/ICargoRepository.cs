using pdtcc_doc_academy.Models;

namespace pdtcc_doc_academy.Repositories
{
    public interface ICargoRepository
    {
        IEnumerable<Cargo> Getall();
        IEnumerable<Cargo> GetById(int id);
        void Add(Cargo cargo);
        void Update(Cargo cargo);
        void Delete(int id);
    }
}
