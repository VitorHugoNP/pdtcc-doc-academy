using Microsoft.EntityFrameworkCore;
using pdtcc_doc_academy.Models;

namespace pdtcc_doc_academy.Repositories
{
    public class CargoRepository : ICargoRepository
    {
        private readonly AppDBContext _dbContext;

        public CargoRepository(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(Cargo cargo)
        {
            try
            {
                _dbContext.Add(cargo);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                await Task.FromException(ex);
            }
        }

        public async Task Delete(Cargo cargo)
        {
            _dbContext.Cargo.Remove(cargo);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Cargo>> Getall()
        {
            return await _dbContext.Cargo.ToListAsync();
        }

        public async Task<Cargo> GetById(int id)
        {
            return await _dbContext.Cargo.FirstOrDefaultAsync(c => c.IdCargo == id);
        }

        public async Task Update(Cargo cargo)
        {
            _dbContext.Cargo.Update(cargo);
            await _dbContext.SaveChangesAsync();
        }
    }
}
