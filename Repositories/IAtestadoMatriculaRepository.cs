using pdtcc_doc_academy.Models;

namespace pdtcc_doc_academy.Repositories
{
    public interface IAtestadoMatriculaRepository
    {
        Task<List<AtestadoMatricula>> GetAll();
        Task<AtestadoMatricula> GetById(int id);
        Task Add(AtestadoMatricula atestadoMatricula);
        Task Update(AtestadoMatricula atestadoMatricula);
        Task Delete(AtestadoMatricula atestadoMatricula);
    }
}
