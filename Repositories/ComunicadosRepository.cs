using Microsoft.EntityFrameworkCore;
using pdtcc_doc_academy.Models;

namespace pdtcc_doc_academy.Repositories
{
    public class ComunicadosRepository : IComunicadosRepository
    {
        private readonly AppDBContext _dbContext;

        public ComunicadosRepository(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(Comunicados comunicados)
        {
            try
            {
                _dbContext.Add(comunicados);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex) 
            {
                await Task.FromException(ex);
            }
        }

        public async Task Delete(Comunicados comunicados)
        {
            _dbContext.Comunicado.Remove(comunicados);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Comunicados>> Getall()
        {
            return await _dbContext.Comunicado.ToListAsync();
        }

        public async Task<Comunicados> GetById(int id)
        {
            return await _dbContext.Comunicado.FirstOrDefaultAsync(c => c.idComunicados == id);
        }

        public async Task Update(Comunicados comunicados)
        {
            _dbContext.Update(comunicados);
            await _dbContext.SaveChangesAsync();
        }
    }
}
