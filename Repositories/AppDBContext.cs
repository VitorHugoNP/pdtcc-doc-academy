using Microsoft.EntityFrameworkCore;
using pdtcc_doc_academy.Models;
using pdtcc_doc_academy.Repositories;

namespace pdtcc_doc_academy.Repositories
{
    public class AppDBContext :DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }

        public DbSet<Alunos> aluno { get; set; }
        public DbSet<AtestadoMatricula> AtestadoMatricula { get; set; }
        public DbSet<Autorizacao> Autorizacao { get; set; }
        public DbSet<Comunicados> Comunicado { get; set; }
        public DbSet<Escola> Escola { get; set; }
        public DbSet<Funcionario> Funcionario { get; set; }
        public DbSet<Protocolo> Protocolo { get; set; }
        public DbSet<AlunoCurso> AlunoCurso { get; set; }
        public DbSet<Curso> Curso { get; set; }
        public DbSet<AlunoSerie> AlunoSerie { get; set; }
        public DbSet<Serie> Serie { get; set; }
        public DbSet<Usuario> Usuario { get; set; }// contexto da tabela usuários do banco de dados

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Alunos>()
                .HasOne(a => a.usuario)
                .WithMany(u => u.alunos)
                .HasForeignKey(a => a.fk_usuario)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Escola>()
                .HasOne(a => a.usuario)
                .WithMany(u => u.escolas)
                .HasForeignKey(a => a.fk_usuario)
                .OnDelete(DeleteBehavior.Restrict);           
           
            modelBuilder.Entity<Funcionario>()
                .HasOne(a => a.usuario)
                .WithMany(u => u.funcionarios)
                .HasForeignKey(a => a.fk_usuario)
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<Funcionario>()
                .HasOne(a => a.escola)
                .WithMany(u => u.funcionarios)
                .HasForeignKey(a => a.fk_escola)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Protocolo>()
                .HasOne(p => p.aluno)
                .WithMany(a => a.protocolos)
                .HasForeignKey(p => p.fk_aluno)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Protocolo>()
                .HasOne(p => p.funcionario)
                .WithMany(a => a.Protocolos)
                .HasForeignKey(p => p.fk_func)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AlunoSerie>()
                .HasOne(al => al.Aluno)
                .WithMany(a => a.alunoSeries)
                .HasForeignKey(al => al.IdAluno);

            modelBuilder.Entity<AlunoSerie>()
                .HasOne(al => al.Serie)
                .WithMany(s => s.AlunoSeries)
                .HasForeignKey(al => al.IdSerie);

            modelBuilder.Entity<Escola>()
                .HasOne(e => e.usuario)
                .WithMany(u => u.escolas)
                .HasForeignKey(e => e.fk_usuario);

            base.OnModelCreating(modelBuilder);

            //    modelBuilder.Entity<Protocolo>().HasKey(p => p.idProtocolo);

            //    // Configurando sem chaves estrangeiras explícitas
            //    modelBuilder.Entity<Protocolo>()
            //        .HasOne(p => p.Aluno)
            //        .WithMany(a => a.Protocolos)
            //        .HasForeignKey(p => p.fk_aluno);

            //    modelBuilder.Entity<Protocolo>()
            //        .HasOne(p => p.Funcionario)
            //        .WithMany(f => f.Protocolos)
            //        .HasForeignKey(p => p.fk_func);

            //    //ALUNO


            //    modelBuilder.Entity<AlunoSerie>()
            //        .HasKey(ase => new { ase.IdAluno, ase.IdSerie });

            //    //Configurar relacionamento NxN entre Alunos e Serie
            //    modelBuilder.Entity<AlunoSerie>()
            //        .HasOne(ase => ase.Aluno)
            //        .WithMany(a => a.AlunoSeries)
            //        .HasForeignKey(ase => ase.IdAluno);

            //    modelBuilder.Entity<AlunoSerie>()
            //        .HasOne(ase => ase.Serie)
            //        .WithMany(a => a.AlunoSeries)
            //        .HasForeignKey(ase => ase.IdSerie);
        }

    }
}
