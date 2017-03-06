using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POC_MVC_Biblioteca.ViewModels
{
    public class CreateUserViewModel
    {
        public int Id { get; set; }
        public int IdSmart { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string AreaDepartamento { get; set; }
        public string Gerente { get; set; }
    }
}