using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using pdtcc_doc_academy.Models;

namespace pdtcc_doc_academy.Repositories
{
    public class FuncionariosRepository : IFuncionariosRepository
    {

        private readonly AppDBContext _dbContext;

        public FuncionariosRepository(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Add(Funcionarios funcionarios)
        {

            try
            {
                _dbContext.Add(funcionarios);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                await Task.FromException(ex);
            }

        }
        //deleta Autorização
        public async Task Delete(Funcionarios funcionarios)
        {
            _dbContext.Funcionario.Remove(funcionarios);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Funcionarios>> Getall()
        {
            return await _dbContext.Funcionario.ToListAsync();
        }

        public async Task<Funcionarios> GetById(int id)
        {
            return await _dbContext.Funcionario.FirstOrDefaultAsync(c => c.idFuncionario == id);
        }

        public async Task<Funcionarios> GetByEmailAndPassword(string email, string senha)
        {
            return await _dbContext.Funcionario
            .FirstOrDefaultAsync(f => f.EmailFuncionario == email && f.SenhaFuncionario == senha);
        }

        public async Task Update(Funcionarios funcionarios)
        {
            _dbContext.Funcionario.Update(funcionarios);
            await _dbContext.SaveChangesAsync();

        }
    }
}
