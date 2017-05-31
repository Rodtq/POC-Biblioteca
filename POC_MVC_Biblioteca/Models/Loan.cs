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
        [Key]
        public int Id { get; set; }
        [ForeignKey("Tenant")]
        public int Id_User { get; set; }
        [ForeignKey("Book")]
        public int Id_Book { get; set; }
        public DateTime LocationlDate { get; set; }
        public DateTime? PullOutDate { get; set; }
        public DateTime? RenewingDate { get; set; }
        public DateTime? DevolutionDate { get; set; }
        public virtual User Tenant { get; set; }
        public virtual Book Book { get; set; }
    }
}