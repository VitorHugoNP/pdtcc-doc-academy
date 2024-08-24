using pdtcc_doc_academy.Models;

namespace pdtcc_doc_academy.Repositories
{
    public interface IComunicadosRepository
    {
        Task<List<Comunicados>> Getall();
        Task<Comunicados> GetById(int id);
        Task Add(Comunicados comunicados);
        Task Update(Comunicados comunicados);
        Task Delete(Comunicados comunicados);
    }
}
