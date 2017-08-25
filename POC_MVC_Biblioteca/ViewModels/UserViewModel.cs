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
        [StringLength(60)]
        [DataType(DataType.Text)]
        [Required(ErrorMessage ="Digite seu Login Smart")]
        [Display(Name = "Login")]
        public string SamAccountName { get; set; }
        [Display(Name = "Código - Id Smart")]
        [Required(ErrorMessage = "Digite seu número de ID")]
        public int IdSmart { get; set; }
        [StringLength(30)]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Digite seu nome")]
        [Display(Name = "Nome")]
        public string Name { get; set; }
        [StringLength(60)]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Digite seu E-mail")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }
        [StringLength(70)]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Digite o nome de sua Área ou departamento")]
        [Display(Name = "Area - Departamento")]
        public string AreaDepartament { get; set; }
        [Required(ErrorMessage = "Digite o nome do seu Gerente")]
        [StringLength(60)]
        [DataType(DataType.Text)]
        [Display(Name = "Gerente")]
        public string Manager { get; set; }
        [StringLength(60)]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Digite a sua função")]
        [Display(Name = "Função")]
        public string Function { get; set; }
        [Required(ErrorMessage = "Digite seu Ramal")]
        [StringLength(60)]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Ramal")]
        public string ExtensionLine { get; set; }
        //Roles
        public int[] RolesId { get; set; }
        [Display(Name = "Perfil")]
        public int[] NewRolesId { get; set; }
        public MultiSelectList Roles { get; set; }
        public string PartialName { get; set; }
        public byte[] Photo { get; set; }

    }



}