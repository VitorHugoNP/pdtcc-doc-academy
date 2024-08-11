using pdtcc_doc_academy.Models;

namespace pdtcc_doc_academy.Repositories
{
    public interface IComunicados
    {
        IEnumerable<Comunicados> Getall();
        IEnumerable<Comunicados> GetById(int id);
        void Add(Comunicados comunicados);
        void Update(Comunicados comunicados);
        void Delete(int id);
    }
}
