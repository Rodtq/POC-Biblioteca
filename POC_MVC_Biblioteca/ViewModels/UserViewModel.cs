using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace POC_MVC_Biblioteca.ViewModels
{
    public class UserViewModel
    {
        public IEnumerable<UserViewModel> UserList { get; set; }
        public int Id { get; set; }
        [StringLength(15)]
        [DataType(DataType.Text)]
        [Required]
        [Display(Name = "Login")]
        public string SamAccountName { get; set; }
        [Display(Name = "Código - Id Smart")]
        public int IdSmart { get; set; }
        [StringLength(30)]
        [DataType(DataType.Text)]
        [Required]
        [Display(Name = "Nome")]
        public string Name { get; set; }
        [StringLength(30)]
        [DataType(DataType.EmailAddress)]
        [Required]
        [Display(Name = "E-mail")]
        public string Email { get; set; }
        [StringLength(30)]
        [DataType(DataType.Text)]
        [Required]
        [Display(Name = "Area - Departamento")]
        public string AreaDepartament { get; set; }
        [StringLength(30)]
        [DataType(DataType.Text)]
        [Display(Name = "Gerente")]
        public string Manager { get; set; }
        [StringLength(30)]
        [DataType(DataType.Text)]
        [Required]
        [Display(Name = "Função")]
        public string Funtion { get; set; }
        [StringLength(30)]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Ramal")]
        public string ExtensionLine { get; set; }
        //Roles
        [Required]
        [Display(Name = "Perfil")]
        public int[] RolesId { get; set; }
        public MultiSelectList Roles { get; set; }
        public string PartialName { get; set; }

    }



}