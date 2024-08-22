using pdtcc_doc_academy.Models;

namespace pdtcc_doc_academy.Repositories
{
    public interface IRequerimentoEquipamentoRepository
    {
        IEnumerable<RequerimentoEquipamento> Getall();
        IEnumerable<RequerimentoEquipamento> GetById(int id);
        void Add(RequerimentoEquipamento requerimentoEquipamento);
        void Update(RequerimentoEquipamento requerimentoEquipamento);
        void Delete(int id);
    }
}
