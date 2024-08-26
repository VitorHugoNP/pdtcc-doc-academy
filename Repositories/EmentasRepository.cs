using Microsoft.EntityFrameworkCore;
using pdtcc_doc_academy.Models;

namespace pdtcc_doc_academy.Repositories
{
    public class EmentasRepository : IEmentasRepository
    {
        private readonly AppDBContext _dbContext;
        public EmentasRepository(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(Ementas ementas)
        {
            try
            {
                _dbContext.Add(ementas);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                await _dbContext.SaveChangesAsync();
            }
         
        }

        public async Task Delete(Ementas ementas)
        {
            _dbContext.Ementas.Remove(ementas);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Ementas>> Getall()
        {
            return await _dbContext.Ementas.ToListAsync();
        }

        public async Task<Ementas> GetById(int id)
        {
            return await _dbContext.Ementas.FirstOrDefaultAsync(c => c.IdEmentas == id);
        }

        public async Task Update(Ementas ementas)
        {
            _dbContext.Ementas.Update(ementas);
            await _dbContext.SaveChangesAsync();
        }
    }
}
