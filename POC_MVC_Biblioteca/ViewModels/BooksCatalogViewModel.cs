using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POC_MVC_Biblioteca.ViewModels
{
    public class BooksCatalogViewModel
    {
        public BooksConsultViewModel ConsultTab { get; set; }
        public CreateBookViewModel RegisterTab { get; set; }
    }
}