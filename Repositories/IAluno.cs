using pdtcc_doc_academy.Models;

namespace pdtcc_doc_academy.Repositories
{
    public interface IAluno
    {
        IEnumerable<Aluno> Getall();
        IEnumerable<Aluno> GetById(int id);
        void Add(Aluno aluno);
        void Update(Aluno aluno);
        void Delete(int id);
    }
}
