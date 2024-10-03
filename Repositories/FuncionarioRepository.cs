﻿using Microsoft.EntityFrameworkCore;
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
            return await _dbContext.Funcionario.FirstOrDefaultAsync(c => c.IdFuncionario == id);
        }

        public async Task<Funcionarios> GetByEmailAndPassword(string nome, string email, string senha)
        {
            //Console.WriteLine($"GetByEmailAndPassword: email={email}, senha={senha}");
            return await _dbContext.Funcionario  
            .FirstOrDefaultAsync(f => f.nome_Func == nome && f.email_Func == email && f.senha_Func == senha);
        }

        public async Task Update(Funcionarios funcionarios)
        {
            _dbContext.Funcionario.Update(funcionarios);
            await _dbContext.SaveChangesAsync();

        }
    }
}