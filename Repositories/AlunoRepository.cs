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
        public async Task Add(Aluno aluno)
        {
            try
            {
                _dbContext.Add(aluno);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                await Task.FromException(ex);
            }

        }
        //deletar Aluno
        public async Task Delete(Aluno aluno)
        {
            _dbContext.Aluno.Remove(aluno);
            await _dbContext.SaveChangesAsync();
        }

        //pega o repositorio Aluno
        public async Task<List<Aluno>> GetAll()
        {
            return await _dbContext.Aluno.ToListAsync();
        }
        //pega o Id do Aluno
        public async Task<Aluno> GetById(int id)
        {
            return await _dbContext.Aluno.FirstOrDefaultAsync(c => c.IdAluno == id);

        }
        public async Task Update(Aluno aluno)
        {
            _dbContext.Aluno.Update(aluno);
            await _dbContext.SaveChangesAsync();
        }
    }
}