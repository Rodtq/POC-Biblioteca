using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace POC_MVC_Biblioteca.Models
{
    public class Locacao
    {
        [Column(Order = 1)Key,ForeignKey("Locatario")]
        public int Id_Usuário { get; set; }
        [Column(Order = 2)Key, ForeignKey("Livro")]
        public int ISBN { get; set; }
        public DateTime DataIninicio { get; set; }
        public DateTime DataRenovacao { get; set; }
        public DateTime DataExpiracao { get; set; }
        public bool Devolucao { get; set; }
        public virtual Usuario Locatario { get; set; }
        public virtual Catalogacao Livro { get; set; }
    }
}