using pdtcc_doc_academy.Models;

namespace pdtcc_doc_academy.Repositories
{
    public interface IFuncionarios
    {
        IEnumerable<Funcionario> Getall();
        IEnumerable <Funcionario> GetById(int id);
        void Add (Funcionario funcionario);
        void Update(Funcionario funcionario);
        void Delete (int id);
    }
}
