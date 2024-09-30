using Microsoft.EntityFrameworkCore;
using pdtcc_doc_academy.Models;
using pdtcc_doc_academy.Repositories;

public class ProtocoloRepository
{
    private readonly AppDBContext _dbContext;

    public ProtocoloRepository(AppDBContext context)
    {
        _dbContext = context;
    }

    // Método para adicionar um novo protocolo
    public async Task AddAsync(Protocolo protocolo)
    {
        await _dbContext.Protocolo.AddAsync(protocolo);
        await _dbContext.SaveChangesAsync();
    }

    // Método para buscar protocolos com relação a Aluno, Funcionario e Professor
    public async Task<List<Protocolo>> GetAllWithRelationsAsync()
    {
        return await _dbContext.Protocolo
            .Include(p => p.Aluno)
            .Include(p => p.Funcionario)
            .ToListAsync();
    }
}
