using pdtcc_doc_academy.Models;

namespace pdtcc_doc_academy.Repositories
{
    public interface IMateriaRepository
    {
        Task<List<Materias>> Getall();
        Task<Materias> GetById(int id);
        Task Add(Materias materia);
        Task Update(Materias materia);
        Task Delete(Materias materias);
    }
}
