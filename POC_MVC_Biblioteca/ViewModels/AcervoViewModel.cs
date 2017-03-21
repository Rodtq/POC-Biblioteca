using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POC_MVC_Biblioteca.ViewModels
{
    public class AcervoViewModel
    {
        public ConsultaLivroViewModel abaConsulta { get; set; }
        public CreateBookViewModel abaCadastro { get; set; }
    }
}