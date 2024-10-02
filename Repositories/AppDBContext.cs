using Microsoft.EntityFrameworkCore;
using pdtcc_doc_academy.Models;
using pdtcc_doc_academy.Repositories;

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
                .WithMany(p => p.Protocolo)
                .HasForeignKey(p => p.idAluno)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Protocolo>()
                .HasOne(p => p.Funcionario)
                .WithMany()
                .HasForeignKey(p => p.idFuncionario)
                .OnDelete(DeleteBehavior.Restrict);

            //ALUNO


            modelBuilder.Entity<AlunoSerie>()
                .HasKey(ase => new { ase.IdAluno, ase.IdSerie });

            // Configurar relacionamento NxN entre Alunos e Serie
            modelBuilder.Entity<AlunoSerie>()
                .HasOne(ase => ase.Aluno)
                .WithMany(/*a => a.AlunoSeries*/)
                .HasForeignKey(ase => ase.IdAluno);

            modelBuilder.Entity<AlunoSerie>()
                .HasOne(ase => ase.Serie)
                .WithMany(/*a => a.AlunoSerie*/)
                .HasForeignKey(ase => ase.IdSerie);
        }

    }
}
