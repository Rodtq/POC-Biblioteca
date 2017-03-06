using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace POC_MVC_Biblioteca.Models
{
    //a many-to-many relationship is a type of cardinality that refers to the relationship between two entities[1] A and B
    //in which A may contain a parent instance for which there are many children in B and vice versa.
    public class Catalogacao
    {
        [Key]
        public int ISBN { get; set; }
        public string TituloDaObra { get; set; }
        public string Autor { get; set; }
        public DateTime AnoLivro { get; set; }
        public string Categoria { get; set; }
        public string Editora { get; set; }
        public int Quantidade { get; set; }
        public string Descricao { get; set; }
        public string Observacao { get; set; }
        public string EstanteLocalizacao { get; set; }
    }
}