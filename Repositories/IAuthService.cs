using pdtcc_doc_academy.Models;

namespace pdtcc_doc_academy.Repositories
{
    public interface IAuthService
    {
        Task<Funcionarios> AuthenticateAsync(string emailFunc, string senhafunc);
    }
}
