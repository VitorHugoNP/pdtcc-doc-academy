using Microsoft.EntityFrameworkCore;
using pdtcc_doc_academy.Models;
using System.Threading.Tasks;

namespace pdtcc_doc_academy.Repositories
{
    public class AlunoSerieRepository : IAlunoSerieRepository
    {
        private readonly AppDBContext _context;

        public AlunoSerieRepository(AppDBContext context)
        {
            _context = context;
        }

        // Método para salvar o Aluno e relacionar com a Série
        public async Task<bool> SaveAlunoWithSerieAsync(Alunos aluno, int idSerie)
        {
            // Verificar se a Serie existe
            var serie = await _context.Serie.FindAsync(idSerie);
            if (serie == null)
            {
                return false;
            }

            // Adicionar o aluno
            await _context.Aluno.AddAsync(aluno);
            await _context.SaveChangesAsync();

            // Relacionar o aluno com a série
            var alunoSerie = new AlunoSerie
            {
                IdAluno = aluno.idAluno,
                IdSerie = idSerie
            };
            await _context.AlunoSerie.AddAsync(alunoSerie);

            // Salvar as alterações
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }
    }
}