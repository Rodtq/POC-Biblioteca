﻿using POC_MVC_Biblioteca.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POC_MVC_Biblioteca.ViewModels
{
    public class BooksViewModel
    {
        public IEnumerable<BookCategory> CataegoriesList { get; set; }
    }
}