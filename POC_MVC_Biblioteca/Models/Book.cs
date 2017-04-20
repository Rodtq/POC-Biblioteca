using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace POC_MVC_Biblioteca.Models
{
    //a many-to-many relationship is a type of cardinality that refers to the relationship between two entities[1] A and B
    //in which A may contain a parent instance for which there are many children in B and vice versa.
    public class Book
    {
        [Key]
        public int Id { get; set; }
        public int ISBN { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime BookYear { get; set; }
        public BookCategory Category { get; set; }
        public string Editor { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
        public string Observation { get; set; }
        public string LocalizationShelf { get; set; }
        public byte[] Cover { get; set; }
    }
}