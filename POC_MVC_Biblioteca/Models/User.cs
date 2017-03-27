using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POC_MVC_Biblioteca.Models
{
    //a many-to-many relationship is a type of cardinality that refers to the relationship between two entities[1] A and B
    //in which A may contain a parent instance for which there are many children in B and vice versa.
    public class User
    {
        public User()
        {
            Roles = new HashSet<Role>();
        }
        public int Id { get; set; }
        public int IdSmart { get; set; }
        public string Name { get; set; }
        public string eMail { get; set; }
        public string Manager { get; set; }
        public string Function { get; set; }
        public string AreaDepartament { get; set; }
        public string ExtensionLine { get; set; }
        public virtual HashSet<Role> Roles { get; set; }
    }
}