using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POC_MVC_Biblioteca.ViewModels
{
    public class CreateBookViewModel
    {
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