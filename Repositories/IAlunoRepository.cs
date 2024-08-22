using pdtcc_doc_academy.Models;

namespace pdtcc_doc_academy.Repositories
{
    public interface IAlunoRepository
    {
        IEnumerable<AlunoRepository> GetAll();
        IEnumerable<AlunoRepository> GetById(int id);
        void Add(AlunoRepository aluno);
        void Update(AlunoRepository aluno);
        void Delete(int id);
    }
}