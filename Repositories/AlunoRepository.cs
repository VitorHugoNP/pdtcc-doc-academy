using pdtcc_doc_academy.Repositories;
using pdtcc_doc_academy.Models;


namespace pdtcc_doc_academy.Repositories
{
    public class AlunoRepository : IAlunoRepository
    {
        private readonly AppDBContext _dbContext;

        public AlunoRepository(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(AlunoRepository aluno)
        {
            _dbContext.Aluno.Add(aluno); // adiciona o aluno ao bd
            _dbContext.SaveChanges(); // salva as mudanças
        }

        public void Delete(int id)
        {
            var aluno = _dbContext.Aluno.Find(id);
            if (aluno != null) { 
                _dbContext.Aluno.Remove(aluno); //remove o id do safado
                _dbContext.SaveChanges(); // salva o bagulho
            }
        }

        public IEnumerable<AlunoRepository> GetAll()
        {
            return _dbContext.Aluno.ToList();
        }

        public IEnumerable<AlunoRepository> GetById(int id)
        {
            var aluno = _dbContext.Aluno.Find(id);
            if (aluno != null)
            {
                return new List<AlunoRepository> { aluno };
            }
            else
            {
                return Enumerable.Empty<AlunoRepository>();
            }
        }

        public void Update(AlunoRepository aluno)
        {
            _dbContext.Aluno.Update(aluno);
            _dbContext.SaveChanges();
        }
    }
}