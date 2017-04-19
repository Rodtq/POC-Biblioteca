using POC_MVC_Biblioteca.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace POC_MVC_Biblioteca.ViewModels
{
    public class BooksConsultViewModel
    {
        public IEnumerable<Book> BooksList { get; set; }
        public int Quantidade { get; set; }
        [StringLength(30)]
        public string TitleFilter { get; set; }
        [StringLength(30)]
        [DataType(DataType.Text)]
        public string AuthorFilter { get; set; }
        public IEnumerable<BookCategory> CataegoriesList { get; set; }
        [StringLength(30)]
        [DataType(DataType.Text)]
        public string CategroryFilter { get; set; }
        [StringLength(30)]
        [DataType(DataType.Text)]
        public string EditorFilter { get; set; }
    }
}