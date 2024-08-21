﻿using Microsoft.EntityFrameworkCore;
using pdtcc_doc_academy.Models;
using SeuProjeto;

namespace pdtcc_doc_academy.Repositories
{
    public class AppDBContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }

        public DbSet<Aluno> Aluno { get; set; }
        public DbSet<AtestadoMatricula> AtestadoMatricula { get; set; }
        public DbSet<Autorizacao> Autorizacao { get; set; }
        public DbSet<Cargo> Cargo { get; set; }
        public DbSet<Comunicados> Comunicado  { get; set; }
        public DbSet<Documentos> Documento  { get; set; }
        public DbSet<Ementas> Ementas  { get; set; }
        public DbSet<Escola> Escola  { get; set; }
        public DbSet<Funcionario> Funcionario  { get; set; }
        public DbSet<GradeCurricular> GradeCurricular  { get; set; }
        public DbSet<Materia> Materia  { get; set; }
        public DbSet<ModeloAtestadoFrequencia> ModeloAtestadoFrequencia  { get; set; }
        public DbSet<Protocolo> Protocolo  { get; set; }
        public DbSet<RequerimentoEquipamento> RequerimentoEquipamento  { get; set; }


    }
}