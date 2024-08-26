using Microsoft.EntityFrameworkCore;
using pdtcc_doc_academy.Models;
using static pdtcc_doc_academy.Repositories.ModeloAtestadoFrequenciaRepository;

namespace pdtcc_doc_academy.Repositories
{
    public class ModeloAtestadoFrequenciaRepository : IModeloAtestadoFrequenciaRepository
    {

        private readonly AppDBContext _dbContext;

        public ModeloAtestadoFrequenciaRepository(AppDBContext dbContext)
        {
                _dbContext = dbContext;
        }

        public async Task Add(ModeloAtestadoFrequencia modeloAtestadoFrequencia)
        {
            try
            {
                _dbContext.Add(modeloAtestadoFrequencia);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                await Task.FromException(ex);
            }
        }

        public async Task Delete(ModeloAtestadoFrequencia modeloAtestadoFrequencia)
        {
            _dbContext.ModeloAtestadoFrequencia.Remove(modeloAtestadoFrequencia);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<ModeloAtestadoFrequencia>> Getall()
        {
            return await _dbContext.ModeloAtestadoFrequencia.ToListAsync();
        }

        public async Task<ModeloAtestadoFrequencia> GetById(int id)
        {
            return await _dbContext.ModeloAtestadoFrequencia.FirstOrDefaultAsync(c => c.IdFrequencia == id);
        }

        public async Task Update(ModeloAtestadoFrequencia modeloAtestadoFrequencia)
        {
            _dbContext.ModeloAtestadoFrequencia.Update(modeloAtestadoFrequencia);
            await _dbContext.SaveChangesAsync();
        }
    }
}
