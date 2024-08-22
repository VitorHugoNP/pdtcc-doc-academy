using pdtcc_doc_academy.Models;

namespace pdtcc_doc_academy.Repositories
{
    public interface IEmentasRepository
    {
        IEnumerable<Ementas> Getall();
        IEnumerable<Ementas> GetById(int id);
        void Add(Ementas ementas);
        void Update(Ementas ementas);
        void Delete(int id);
    }
}
