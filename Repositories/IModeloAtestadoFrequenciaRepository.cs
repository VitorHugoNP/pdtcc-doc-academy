using pdtcc_doc_academy.Models;

namespace pdtcc_doc_academy.Repositories
{
    public interface IModeloAtestadoFrequenciaRepository
    {
        IEnumerable<ModeloAtestadoFrequencia> Getall();
        IEnumerable<ModeloAtestadoFrequencia> GetById(int id);
        void Add(ModeloAtestadoFrequencia modeloAtestadoFrequencia);
        void Update(ModeloAtestadoFrequencia modeloAtestadoFrequencia);
        void Delete(int id);
    }
}
