using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POC_MVC_Biblioteca.Models
{
    public class Role
    {
        public Role()
        {
            Users = new HashSet<User>();    
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual HashSet<User> Users { get; set; }
    }
}