using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POC_MVC_Biblioteca.ViewModels
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public int IdSmart { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string AreaDepartament { get; set; }
        public string Manager { get; set; }
        public string Funtion { get; set; }
        public string ExtensionLine { get; set; }
    }
}