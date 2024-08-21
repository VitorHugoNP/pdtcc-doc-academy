﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pdtcc_doc_academy.Models
{
    public class Comunicados
    {
        [Key]
        public int IdComunicados { get; set; }
        [Required]
        public DateTime dataComunicado { get; set; }
        [ForeignKey("doc")]
        public int fkDoc { get; set; }
    }
}