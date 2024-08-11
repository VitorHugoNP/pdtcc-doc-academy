using pdtcc_doc_academy.Models;

namespace pdtcc_doc_academy.Repositories
{
    public interface IMateria
    {
        IEnumerable<Materia> Getall();
        IEnumerable<Materia> GetById(int id);
        void Add(Materia materia);
        void Update(Materia materia);
        void Delete(int id);
    }
}
