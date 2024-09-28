using pdtcc_doc_academy.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace pdtcc_doc_academy.Repositories
{
    public interface IAlunoRepository
    {
        Task Add(Alunos aluno);
        Task Delete(Alunos aluno);
        Task<List<Alunos>> GetAll();
        Task<Alunos> GetByDataForLogin(string nome, int cpf, string curso, int rm, string senha);
        Task<Alunos> GetById(int id);
        Task Update(Alunos aluno);
    }
}