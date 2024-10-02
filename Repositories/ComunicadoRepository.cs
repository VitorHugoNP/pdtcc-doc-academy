using Microsoft.EntityFrameworkCore;
using pdtcc_doc_academy.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace pdtcc_doc_academy.Repositories
{
    public class ComunicadoRepository : IComunicadoRepository
    {
        private readonly AppDBContext _dbContext;

        public ComunicadoRepository(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(Comunicados comunicado)
        {
            try
            {
                _dbContext.Add(comunicado);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao adicionar comunicado", ex);
            }
        }

        public async Task Delete(Comunicados comunicado)
        {
            _dbContext.Comunicado.Remove(comunicado);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Comunicados>> GetAll()
        {
            return await _dbContext.Comunicado.ToListAsync();
        }

        public async Task<Comunicados> GetById(int id)
        {
            return await _dbContext.Comunicado.FirstOrDefaultAsync(c => c.idComunicados == id);
        }

        public async Task Update(Comunicados comunicado)
        {
            _dbContext.Comunicado.Update(comunicado);
            await _dbContext.SaveChangesAsync();
        }
    }
}