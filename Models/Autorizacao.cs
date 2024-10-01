﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pdtcc_doc_academy.Models
{
    public class Autorizacao
    {
        [Key]
        public int IdAutorizacao { get; set; }
        public DateTime? dataAut { get; set; }
        [ForeignKey("fk_prot")]
        public int fk_prot { get; set; }
        public virtual Protocolo Protocolo { get; set; }
    }
}
