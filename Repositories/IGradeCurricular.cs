using pdtcc_doc_academy.Models;

namespace pdtcc_doc_academy.Repositories
{
    public interface IGradeCurricular
    {
        IEnumerable<GradeCurricular> Getall();
        IEnumerable<GradeCurricular> GetById(int id);
        void Add(GradeCurricular gradeCurricular);
        void Update(GradeCurricular gradeCurricular);
        void Delete(int id);
    }
}
