using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace POC_MVC_Biblioteca.ViewModels
{
    public class UserViewModel
    {
        public IEnumerable<UserViewModel> UserList { get; set; }
        public int Id { get; set; }
        public string SamAccountName { get; set; }
        public int IdSmart { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string AreaDepartament { get; set; }
        public string Manager { get; set; }
        public string Funtion { get; set; }
        public string ExtensionLine { get; set; }
        //Roles
        public int[] RolesId { get; set; }
        public MultiSelectList Roles { get; set; }
        public string PartialName { get; set; }

    }



}