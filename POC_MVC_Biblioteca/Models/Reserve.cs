using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace POC_MVC_Biblioteca.Models
{
    public class Reserve
    {
        [Column(Order = 1) Key, ForeignKey("User")]
        public int Id_User { get; set; }
        [Column(Order = 2) Key, ForeignKey("Book")]
        public int ISBN { get; set; }
        public virtual User User { get; set; }
        public virtual Book Book { get; set; }
    }
}