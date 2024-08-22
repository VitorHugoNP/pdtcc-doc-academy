using pdtcc_doc_academy.Models;

namespace pdtcc_doc_academy.Repositories
{
    public interface IAtestadoMatriculaRepository
    {
        IEnumerable<AtestadoMatricula> Getall();
        IEnumerable<AtestadoMatricula> GetById(int id);
        void Add(AtestadoMatricula atestadoMatricula);
        void Update(AtestadoMatricula atestadoMatricula);
        void Delete(int id);
    }
}
