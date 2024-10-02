using Microsoft.EntityFrameworkCore;
using pdtcc_doc_academy.Models;
using pdtcc_doc_academy.Repositories;

public class ProtocoloRepository :IProtocoloRepository
{
    private readonly AppDBContext _dbContext;

    public ProtocoloRepository(AppDBContext context)
    {
        _dbContext = context;
    }

    public async Task Add(Protocolo protocolo)
    {
         _dbContext.Protocolo.AddAsync(protocolo);
        await _dbContext.SaveChangesAsync();
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

    // Método para buscar protocolos com relação a Aluno, Funcionario e Professor
    //public async Task<List<Protocolo>> GetAllWithRelationsAsync()
    //{
    //    return await _dbContext.Protocolo
    //        .Include(p => p.Aluno)
    //        .Include(p => p.Funcionario)
    //        .ToListAsync();
    //}

    public async Task<Protocolo> GetById(int id)
    {
        return await _dbContext.Protocolo.FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task Update(Protocolo protocolo)
    {
        _dbContext.Protocolo.Update(protocolo);
        await _dbContext.SaveChangesAsync();
    }
}
