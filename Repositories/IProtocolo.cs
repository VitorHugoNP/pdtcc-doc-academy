using pdtcc_doc_academy.Models;
using SeuProjeto;

namespace pdtcc_doc_academy.Repositories
{
    public interface IProtocolo
    {
        IEnumerable<Protocolo> Getall();
        IEnumerable<Protocolo> GetById(int id);
        void Add(Protocolo protocolo);
        void Update(Protocolo protocolo);
        void Delete(int id);
    }
}
