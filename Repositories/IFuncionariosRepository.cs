using pdtcc_doc_academy.Models;

namespace pdtcc_doc_academy.Repositories
{
    public interface IFuncionariosRepository
    {
        IEnumerable<Funcionarios> Getall();
        IEnumerable <Funcionarios> GetById(int id);
        void Add (Funcionarios funcionario);
        void Update(Funcionarios funcionario);
        void Delete (int id);
    }
}
