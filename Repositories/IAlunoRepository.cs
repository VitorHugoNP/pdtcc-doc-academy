using pdtcc_doc_academy.Models;

namespace pdtcc_doc_academy.Repositories
{
    public interface IAlunoRepository
    {
        Task <List<Aluno>> GetAll();
        Task <Aluno> GetById(int id);
        Task Add(Aluno aluno);
        Task Update(Aluno aluno);
        Task Delete(Aluno aluno);
    }
}