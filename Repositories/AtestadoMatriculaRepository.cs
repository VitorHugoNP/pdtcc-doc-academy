using Microsoft.EntityFrameworkCore;
using pdtcc_doc_academy.Models;

namespace pdtcc_doc_academy.Repositories
{
    public class AtestadoMatriculaRepository : IAtestadoMatriculaRepository
    {
        private readonly AppDBContext _dbContext;

        public AtestadoMatriculaRepository(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        //Adiciona Atestado
        public async Task Add(AtestadoMatricula atestadoMatricula)
        {
            try
            {
                _dbContext.Add(atestadoMatricula);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                await Task.FromException(ex);
            }

        }
        //deletar Atestado
        public async Task Delete(AtestadoMatricula atestadoMatricula)
        {
            _dbContext.AtestadoMatricula.Remove(atestadoMatricula);
            await _dbContext.SaveChangesAsync();
        }

        //pega o repositorio AtestadoMatricula
        public async Task<List<AtestadoMatricula>> GetAll()
        {
            return await _dbContext.AtestadoMatricula.ToListAsync();
        }

        //pega o Id do Atestado
        public async Task<AtestadoMatricula> GetById(int id)
        {
            return await _dbContext.AtestadoMatricula.FirstOrDefaultAsync(c => c.IdAtest_mat == id);

        }
        public async Task Update(AtestadoMatricula atestadomatricula)
        {
            _dbContext.AtestadoMatricula.Update(atestadomatricula);
            await _dbContext.SaveChangesAsync();
        }
    }
}
