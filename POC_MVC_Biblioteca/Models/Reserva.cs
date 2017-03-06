using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace POC_MVC_Biblioteca.Models
{
    public class Reserva
    {
        [Column(Order = 1) Key, ForeignKey("Usuario")]
        public int Id_Usuario { get; set; }
        [Column(Order = 2) Key, ForeignKey("Livro")]
        public int ISBN { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual Catalogacao Livro { get; set; }
    }
}