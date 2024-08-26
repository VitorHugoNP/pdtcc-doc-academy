using pdtcc_doc_academy.Models;

namespace pdtcc_doc_academy.Repositories
{
    public interface IEmentasRepository
    {
        Task<List<Ementas>> Getall();
        Task<Ementas> GetById(int id);
        Task Add(Ementas ementas);
        Task Update(Ementas ementas);
        Task Delete(Ementas ementas);
    }
}
