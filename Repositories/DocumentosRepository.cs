using Microsoft.EntityFrameworkCore;
using pdtcc_doc_academy.Models;

namespace pdtcc_doc_academy.Repositories
{
    public class DocumentosRepository : IDocumentosRepository
    {
        private readonly AppDBContext _dbContext;

        public DocumentosRepository(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(Documentos documentos)
        {
            try
            {
                _dbContext.Add(documentos);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                await Task.FromException(ex);
            }
        }

        public async Task Delete(Documentos documentos)
        {
            _dbContext.Documento.Remove(documentos);
            _dbContext?.SaveChangesAsync();
        }

        public Task<List<Documentos>> Getall()
        {
            return _dbContext.Documento.ToListAsync();
        }

        public async Task<Documentos> GetById(int id)
        {
            return await _dbContext.Documento.FirstOrDefaultAsync(c => c.IdDoc == id);
        }

        public async Task Update(Documentos documentos)
        {
            _dbContext.Documento.Update(documentos);
            _dbContext.SaveChanges();
        }
    }
}
