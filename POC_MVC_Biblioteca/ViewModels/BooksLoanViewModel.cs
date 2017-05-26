using POC_MVC_Biblioteca.Models;
using System;
using System.Collections.Generic;

using System.Web.Mvc;

namespace POC_MVC_Biblioteca.ViewModels
{
    public class BooksLoanViewModel
    {

        public int UserId { get; set; }
        public string UserName { get; set; }
        public int BookId { get; set; }
        public string BookName { get; set; }
        public DateTime InitialDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public IEnumerable<BooksLoanViewModel> BookLoanList { get; set; }
    }
}