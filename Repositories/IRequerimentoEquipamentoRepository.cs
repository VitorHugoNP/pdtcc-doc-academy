using pdtcc_doc_academy.Models;

namespace pdtcc_doc_academy.Repositories
{
    public interface IRequerimentoEquipamentoRepository
    {
        Task<List<RequerimentoEquipamento>> Getall();
        Task<RequerimentoEquipamento> GetById(int id);
        Task Add(RequerimentoEquipamento requerimentoEquipamento);
        Task Update(RequerimentoEquipamento requerimentoEquipamento);
        Task Delete(RequerimentoEquipamento requerimentoEquipamento);
    }
}
