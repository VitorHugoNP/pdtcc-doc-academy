using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using pdtcc_doc_academy.Models;

namespace pdtcc_doc_academy.Repositories
{
    public interface IFuncionariosRepository
    {
        Task <List<Funcionarios>> Getall();
        Task <Funcionarios> GetById(int id);
        Task Add (Funcionarios funcionario);
        Task Update(Funcionarios funcionario);
        Task Delete (Funcionarios funcionarios);
        // Implementação do método de login
        Task<Funcionarios> GetByUsernameAndPassword(string email, string senha);
    }
}
