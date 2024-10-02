using Microsoft.EntityFrameworkCore;
using pdtcc_doc_academy.Models;

namespace pdtcc_doc_academy.Repositories
{
    public class AutorizacaoRepository : IAutorizacaoRepository
    {
        private readonly AppDBContext _dbContext;

        public AutorizacaoRepository(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(Autorizacao autorizacao)
        {
            try
            {
                _dbContext.Add(autorizacao);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                await Task.FromException(ex);
            }

        }

        public async Task Delete(Autorizacao autorizacao)
        {
            _dbContext.Autorizacao.Remove(autorizacao);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Autorizacao>> GetAll()
        {
            return await _dbContext.Autorizacao.ToListAsync();
        }

        public async Task<Autorizacao> GetById(int id)
        {
            return await _dbContext.Autorizacao.FirstOrDefaultAsync(c => c.IdAutorizacao == id);

        }

        public async Task Update(Autorizacao autorizacao)
        {
            _dbContext.Autorizacao.Update(autorizacao);
            await _dbContext.SaveChangesAsync();
        }
    }
}
