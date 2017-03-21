using POC_MVC_Biblioteca.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace POC_MVC_Biblioteca.ViewModels
{
    public class ConsultaLivroViewModel
    {
        public IEnumerable<Catalogacao> ListaLivros { get; set; }
        public int Quantidade { get; set; }
        public string FiltroTitulo { get; set; }
        public string FiltroAutor { get; set; }
        public IEnumerable<SelectListItem> ListaCategorias { get; set; }
        public string FiltroCategoria { get; set; }
        public string FiltroEditora { get; set; }
    }
}