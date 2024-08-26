using Microsoft.EntityFrameworkCore;
using pdtcc_doc_academy.Models;

namespace pdtcc_doc_academy.Repositories
{
    public class RequerimentoEquipamentoRepository : IRequerimentoEquipamentoRepository
    {
        private readonly AppDBContext _dbContext;
        
        public RequerimentoEquipamentoRepository(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Add(RequerimentoEquipamento requerimentoEquipamento)
        {
            try
            {
                _dbContext.Add(requerimentoEquipamento);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                await Task.FromException(ex);
            }

        }

        public async Task Delete(RequerimentoEquipamento requerimentoEquipamento)
        {
            _dbContext.RequerimentoEquipamento.Remove(requerimentoEquipamento);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<RequerimentoEquipamento>> Getall()
        {
            return await _dbContext.RequerimentoEquipamento.ToListAsync();
        }

        public async Task<RequerimentoEquipamento> GetById(int id)
        {
            return await _dbContext.RequerimentoEquipamento.FirstOrDefaultAsync(c => c.IdReq == id);
        }

        public async Task Update(RequerimentoEquipamento requerimentoEquipamento)
        {
            _dbContext.RequerimentoEquipamento.Update(requerimentoEquipamento);
            await _dbContext.SaveChangesAsync();
        }
    }
}
