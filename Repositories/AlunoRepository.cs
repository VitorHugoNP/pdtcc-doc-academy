using pdtcc_doc_academy.Repositories;
using pdtcc_doc_academy.Models;
using Microsoft.EntityFrameworkCore;

namespace pdtcc_doc_academy.Repositories
{
    public class AlunoRepository : IAlunoRepository
    {
        private readonly AppDBContext _dbContext;

        public AlunoRepository(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        //Adiciona Aluno
        public async Task Add(Alunos aluno)
        {
            try
            {
                _dbContext.Aluno.Add(aluno);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                await Task.FromException(ex);
            }

        }
        //deletar Aluno
        public async Task Delete(Alunos aluno)
        {
            _dbContext.Aluno.Remove(aluno);
            await _dbContext.SaveChangesAsync();
        }

        //pega o repositorio Aluno
        public async Task<List<Alunos>> GetAll()
        {
            return await _dbContext.Aluno.ToListAsync();
        }

        public async Task<Alunos> GetByDataForLogin(string nome, int cpf, int rg, int rm, string email, string senha)
        {
            return await _dbContext.Aluno.FirstOrDefaultAsync(
                a => a.nomeAluno == nome
                && a.cpfAluno == cpf
                && a.rgAluno == rg
                && a.rmAluno == rm
                && a.emailAluno == email
                && a.senhaAluno == senha
            );
        }

        //pega o Id do Aluno
        public async Task<Alunos> GetById(int id)
        {
            return await _dbContext.Aluno.FirstOrDefaultAsync(c => c.idAluno == id);

        }
        public async Task Update(Alunos aluno)
        {
            _dbContext.Aluno.Update(aluno);
            await _dbContext.SaveChangesAsync();
        }
    }
}