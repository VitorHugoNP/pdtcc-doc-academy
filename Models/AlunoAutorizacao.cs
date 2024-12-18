﻿using System;
using System.ComponentModel.DataAnnotations;

namespace pdtcc_doc_academy.Models
{
    public class AlunoAutorizacao
    {
        // do Aluno
        public int idAluno { get; set; } // Renomeado para idAluno

        public string nomeAluno { get; set; }
        public int cpfAluno { get; set; }
        public int rgAluno { get; set; }
        public int rmAluno { get; set; }

        //do Protocolo
        public string tipo_Doc { get; set; }

        public int idAutorizacao { get; set; }
        public DateTime? data_aut { get; set; }
    }
}