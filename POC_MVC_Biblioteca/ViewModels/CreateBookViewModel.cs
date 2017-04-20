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
        [Required]
        [Display(Name = "ISBN")]
        public int ISBN { get; set; }
        [StringLength(30)]
        [DataType(DataType.Text)]
        [Required]
        [Display(Name = "Titulo")]
        public string Title { get; set; }
        [StringLength(30)]
        [DataType(DataType.Text)]
        [Required]
        [Display(Name = "Autor")]
        public string Author { get; set; }
        [DataType(DataType.Date)]
        [Required]
        [Display(Name = "Data")]
        public DateTime BookYear { get; set; }
        [Required]
        [Display(Name = "Categoria")]
        public string CategoryId { get; set; }
        [StringLength(30)]
        [DataType(DataType.Text)]
        [Required]
        [Display(Name = "Editora")]
        public string Editor { get; set; }
        [Required]
        [Display(Name = "Categoria")]
        public int Quantity { get; set; }
        [StringLength(100)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Descrição")]
        public string Description { get; set; }
        [StringLength(100)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Observação")]
        public string Observation { get; set; }
        public string LocalizationShelf { get; set; }
        public IEnumerable<SelectListItem> BookCategories { get; set; }
    }
}