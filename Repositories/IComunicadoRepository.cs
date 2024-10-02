using pdtcc_doc_academy.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace pdtcc_doc_academy.Repositories
{
    public interface IComunicadoRepository
    {
        Task Add(Comunicados comunicado);
        Task Delete(Comunicados comunicado);
        Task<List<Comunicados>> GetAll();
        Task<Comunicados> GetById(int id);
        Task Update(Comunicados comunicado);
    }
}