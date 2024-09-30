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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Protocolo>().HasKey(p => p.Id);

            // Configurando sem chaves estrangeiras explícitas
            modelBuilder.Entity<Protocolo>()
                .HasOne(p => p.Aluno)
                .WithMany() // Aluno pode ter vários Protocolos
                .HasForeignKey(p => p.Id_Aluno)
                .OnDelete(DeleteBehavior.Restrict); // Sem deletar cascata

            modelBuilder.Entity<Protocolo>()
                .HasOne(p => p.Funcionario)
                .WithMany()
                .HasForeignKey(p => p.Id_Funcionario)
                .OnDelete(DeleteBehavior.Restrict);
        }

    }
}
