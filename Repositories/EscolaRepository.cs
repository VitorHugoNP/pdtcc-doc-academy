using Microsoft.EntityFrameworkCore;
using pdtcc_doc_academy.Models;
using System.Linq.Expressions;

namespace pdtcc_doc_academy.Repositories
{
    public class EscolaRepository : IEscolaRepository
    {

        private readonly AppDBContext _dbContext;

        public EscolaRepository(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(Escolas escola)
        {
            try
            {
                _dbContext.Add(escola);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task Delete(Escolas escola)
        {
            _dbContext.Escola.Remove(escola);
        }

        public async Task<List<Escolas>> Getall()
        {
            return await _dbContext.Escola.ToListAsync();
        }

        public async Task<Escolas> GetById(int id)
        {
            return await _dbContext.Escola.FirstOrDefaultAsync(c => c.IdEscola == id);
        }

        public async Task Update(Escolas escola)
        {
            _dbContext.Escola.Update(escola);
            await _dbContext.SaveChangesAsync();
        }
    }
}
