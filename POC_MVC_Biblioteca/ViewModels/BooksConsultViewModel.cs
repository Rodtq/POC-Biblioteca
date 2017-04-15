using POC_MVC_Biblioteca.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace POC_MVC_Biblioteca.ViewModels
{
    public class BooksConsultViewModel
    {
        public IEnumerable<Book> BooksList { get; set; }
        public int Quantidade { get; set; }
        public string TitleFilter { get; set; }
        public string AuthorFilter { get; set; }
        public IEnumerable<BookCategory> CataegoriesList { get; set; }
        public string CategroryFilter { get; set; }
        public string EditorFilter { get; set; }
    }
}