using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace POC_MVC_Biblioteca.ViewModels
{
    public class CreateBookViewModel
    {
        public int ISBN { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime BookYear { get; set; }
        public string CategoryId { get; set; }
        public string Editor { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
        public string Observation { get; set; }
        public string LocalizationShelf { get; set; }
        public IEnumerable<SelectListItem> BookCategories { get; set; }
    }
}