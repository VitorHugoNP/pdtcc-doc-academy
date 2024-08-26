using Microsoft.EntityFrameworkCore;
using pdtcc_doc_academy.Models;

namespace pdtcc_doc_academy.Repositories
{
    public class GradeCurricularRepository : IGradeCurricularRepository
    {
        private readonly AppDBContext _dbContext;

        public GradeCurricularRepository(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(GradeCurricular gradeCurricular)
        {
            try
            {
                _dbContext.Add(gradeCurricular);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                await Task.FromException(ex);
            }
        }

        public async Task Delete(GradeCurricular gradeCurricular)
        {
            _dbContext.GradeCurricular.Remove(gradeCurricular);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<GradeCurricular>> Getall()
        {
            return await _dbContext.GradeCurricular.ToListAsync();
        }

        public async Task<GradeCurricular> GetById(int id)
        {
            return await _dbContext.GradeCurricular.FirstOrDefaultAsync(c => c.IdGradeCurricular == id);
        }

        public async Task Update(GradeCurricular gradeCurricular)
        {
            _dbContext.GradeCurricular.Update(gradeCurricular);
            await _dbContext.SaveChangesAsync();
        }
    }
}
