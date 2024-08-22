using pdtcc_doc_academy.Models;

namespace pdtcc_doc_academy.Repositories
{
    public interface IMateriaRepository
    {
        IEnumerable<Materias> Getall();
        IEnumerable<Materias> GetById(int id);
        void Add(Materias materia);
        void Update(Materias materia);
        void Delete(int id);
    }
}
