using Microsoft.EntityFrameworkCore;
using SeuProjeto;

namespace pdtcc_doc_academy.Repositories
{
    public class ProtocoloRepository : IProtocoloRepository
    {
        private readonly AppDBContext _dbContext;

        public ProtocoloRepository(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Add(Protocolo protocolo)
        {
            try
            {
                _dbContext.Add(protocolo);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                await Task.FromException(ex);
            }

        }

        public async Task Delete(Protocolo protocolo)
        {
            _dbContext.Protocolo.Remove(protocolo);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Protocolo>> Getall()
        {
            return await _dbContext.Protocolo.ToListAsync();
        }

        public async Task<Protocolo> GetById(int id)
        {
            return await _dbContext.Protocolo.FirstOrDefaultAsync(c => c.ProtocoloId == id);
        }

        public async Task Update(Protocolo protocolo)
        {
            _dbContext.Protocolo.Update(protocolo);
            await _dbContext.SaveChangesAsync();
        }
    }
}
