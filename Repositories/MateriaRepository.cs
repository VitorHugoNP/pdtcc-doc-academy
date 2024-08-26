using Microsoft.EntityFrameworkCore;
using pdtcc_doc_academy.Models;

namespace pdtcc_doc_academy.Repositories
{
    public class MateriaRepository : IMateriaRepository
    {
        private readonly AppDBContext _dbContext;

        public MateriaRepository(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(Materias materia)
        {
            try
            {
                _dbContext.Add(materia);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                await Task.FromException(ex);
            }

        }

        public async Task Delete(Materias materia)
        {
            _dbContext.Materia.Remove(materia);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Materias>> Getall()
        {
            return await _dbContext.Materia.ToListAsync();
        }

        public async Task<Materias> GetById(int id)
        {
            return await _dbContext.Materia.FirstOrDefaultAsync(c => c.IdMateria == id);
        }

        public async Task Update(Materias materia)
        {
            _dbContext.Materia.Update(materia);
            await _dbContext.SaveChangesAsync();
        }
    }
}
