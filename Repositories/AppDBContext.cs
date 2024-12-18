﻿using Microsoft.EntityFrameworkCore;
using pdtcc_doc_academy.Models;
using pdtcc_doc_academy.Repositories;

namespace pdtcc_doc_academy.Repositories
{
    public class AppDBContext :DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }

        public DbSet<Alunos> aluno { get; set; }
        public DbSet<Atestado_Matricula> Atestado_Matricula { get; set; }
        public DbSet<Autorizacao> Autorizacao { get; set; }
        public DbSet<Comunicados> Comunicados { get; set; }
        public DbSet<Escola> Escola { get; set; }
        public DbSet<Funcionario> Funcionario { get; set; }
        public DbSet<Protocolo> Protocolo { get; set; }
        public DbSet<AlunoCurso> aluno_curso { get; set; }
        public DbSet<Curso> Curso { get; set; }
        public DbSet<AlunoSerie> aluno_serie { get; set; }
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

            ///////////////////////////////////////

            //aluno Série

            modelBuilder.Entity<AlunoSerie>()
            .HasKey(ac => new { ac.fk_aluno, ac.fk_serie }); // Chave primária composta

            modelBuilder.Entity<AlunoSerie>()
                .HasOne(al => al.Aluno)
                .WithMany(a => a.alunoSeries)
                .HasForeignKey(al => al.fk_aluno);

            modelBuilder.Entity<AlunoSerie>()
                .HasOne(al => al.Serie)
                .WithMany(s => s.AlunoSeries)
                .HasForeignKey(al => al.fk_serie);

            //Aluno curso

            modelBuilder.Entity<AlunoCurso>()
                .HasKey(ac => new { ac.fk_aluno, ac.fk_curso }); // Chave primária composta

            modelBuilder.Entity<AlunoCurso>()
                .HasOne(ac => ac.Aluno)
                .WithMany(a => a.alunoCursos) // Supondo que Alunos tenha uma coleção de AlunoCursos
                .HasForeignKey(ac => ac.fk_aluno);

            modelBuilder.Entity<AlunoCurso>()
                .HasOne(ac => ac.Curso)
                .WithMany(c => c.AlunoCursos) // Supondo que Curso tenha uma coleção de AlunoCursos
                .HasForeignKey(ac => ac.fk_curso);

            ///////////////////////////////////////

            modelBuilder.Entity<Escola>()
                .HasOne(e => e.usuario)
                .WithMany(u => u.escolas)
                .HasForeignKey(e => e.fk_usuario);



            modelBuilder.Entity<Autorizacao>()
                .HasOne(p => p.Protocolo)
                .WithMany(aut => aut.autorizacao)
                .HasForeignKey(p => p.fk_prot);

            modelBuilder.Entity<Atestado_Matricula>()
                .HasOne(p => p.Protocolo)
                .WithMany(at => at.Atestado_Matricula)
                .HasForeignKey(p => p.fk_prot);

            modelBuilder.Entity<Comunicados>()
                .HasOne(p => p.Protocolo)
                .WithMany(com => com.comunicados)
                .HasForeignKey(p => p.fk_prot);

            base.OnModelCreating(modelBuilder);
        }

    }
}
