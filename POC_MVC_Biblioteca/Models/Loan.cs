using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace POC_MVC_Biblioteca.Models
{
    public class Loan
    {
        [Column(Order = 1)Key,ForeignKey("Tenant")]
        public int Id_User { get; set; }
        [Column(Order = 2)Key, ForeignKey("Book")]
        public int ISBN { get; set; }
        public DateTime InitialDate { get; set; }
        public DateTime RenewingDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool Devolution { get; set; }
        public virtual User Tenant { get; set; }
        public virtual Book Book { get; set; }
    }
}