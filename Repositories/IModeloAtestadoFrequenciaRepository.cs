using pdtcc_doc_academy.Models;

namespace pdtcc_doc_academy.Repositories
{
    public interface IModeloAtestadoFrequenciaRepository
    {
        Task<List<ModeloAtestadoFrequencia>> Getall();
        Task<ModeloAtestadoFrequencia> GetById(int id);
        Task Add(ModeloAtestadoFrequencia modeloAtestadoFrequencia);
        Task Update(ModeloAtestadoFrequencia modeloAtestadoFrequencia);
        Task Delete(ModeloAtestadoFrequencia modeloAtestadoFrequencia);
    }
}
