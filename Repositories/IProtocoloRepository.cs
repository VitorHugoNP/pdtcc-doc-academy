using pdtcc_doc_academy.Models;
using SeuProjeto;

namespace pdtcc_doc_academy.Repositories
{
    public interface IProtocoloRepository
    {
        Task<List<Protocolo>> Getall();
        Task<Protocolo> GetById(int id);
        Task Add(Protocolo protocolo);
        Task Update(Protocolo protocolo);
        Task Delete(Protocolo protocolo);
    }
}
