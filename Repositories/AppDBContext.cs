using Microsoft.EntityFrameworkCore;
using pdtcc_doc_academy.Models;

namespace pdtcc_doc_academy.Repositories
{
    public class AppDBContext :DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }

        public DbSet<Alunos> Aluno { get; set; }
        public DbSet<AtestadoMatricula> AtestadoMatricula { get; set; }
        public DbSet<Autorizacao> Autorizacao { get; set; }
        public DbSet<Comunicados> Comunicado { get; set; }
        public DbSet<Escolas> Escola { get; set; }
        public DbSet<Funcionarios> Funcionario { get; set; }
        public DbSet<Protocolo> Protocolo { get; set; }
        public DbSet<AlunoCurso> AlunoCurso { get; set; }
        public DbSet<Curso> Curso { get; set; }
        public DbSet<AlunoSerie> AlunoSerie { get; set; }
        public DbSet<Serie> Serie { get; set; }

    }
}
