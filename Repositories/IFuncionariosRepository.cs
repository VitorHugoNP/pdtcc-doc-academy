using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using pdtcc_doc_academy.Models;

namespace pdtcc_doc_academy.Repositories
{
    public interface IFuncionariosRepository
    {
        Task <List<funcionario>> Getall();
        Task <funcionario> GetById(int id);
        Task Add (funcionario funcionario);
        Task Update(funcionario funcionario);
        Task Delete (funcionario funcionarios);
        // Implementação do método de login
        Task<funcionario> GetByEmailAndPassword(string nome, string email, string senha);
    }
}
