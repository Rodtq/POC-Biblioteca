using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POC_MVC_Biblioteca.ViewModels
{
    public class BooksReserveViewModel
    {
        public int Id_User { get; set; }
        public int Id_Book { get; set; }
        public DateTime? PullOutlDate { get; set; }
    }
}