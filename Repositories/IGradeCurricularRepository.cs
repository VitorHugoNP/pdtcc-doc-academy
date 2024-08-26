using pdtcc_doc_academy.Models;

namespace pdtcc_doc_academy.Repositories
{
    public interface IGradeCurricularRepository
    {
        Task<List<GradeCurricular>> Getall();
        Task<GradeCurricular> GetById(int id);
        Task Add(GradeCurricular gradeCurricular);
        Task Update(GradeCurricular gradeCurricular);
        Task Delete(GradeCurricular gradeCurricular);
    }
}
