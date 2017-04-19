using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace POC_MVC_Biblioteca.ViewModels
{
    public class CreateBookViewModel
    {
        public int ISBN { get; set; }
        [StringLength(30)]
        [DataType(DataType.Text)]
        [Required]
        public string Title { get; set; }
        [StringLength(30)]
        [DataType(DataType.Text)]
        [Required]
        public string Author { get; set; }
        [StringLength(30)]
        [DataType(DataType.Date)]
        [Required]
        public DateTime BookYear { get; set; }
        public string CategoryId { get; set; }
        [StringLength(30)]
        [DataType(DataType.Text)]
        [Required]
        public string Editor { get; set; }
        [Required]
        public int Quantity { get; set; }
        [StringLength(100)]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        [StringLength(100)]
        [DataType(DataType.MultilineText)]
        public string Observation { get; set; }
        public string LocalizationShelf { get; set; }
        public IEnumerable<SelectListItem> BookCategories { get; set; }
    }
}