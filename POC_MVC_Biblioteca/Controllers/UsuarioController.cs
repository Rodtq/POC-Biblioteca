using POC_MVC_Biblioteca.Models;
using POC_MVC_Biblioteca.Services;
using POC_MVC_Biblioteca.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace POC_MVC_Biblioteca.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly UserManager _um;
        public UsuarioController()
        {
            _um = new UserManager();
        }
        
        // GET: Usuario
        public ActionResult Index()
        {
            UserManager um = new UserManager();
            AllUsersViewModel response = new AllUsersViewModel
            {
                UserList = um.GetAllUser()
            };
            return View();
        }

        public ActionResult GetAllUsers()
        {
            UserManager um = new UserManager();
            AllUsersViewModel response = new AllUsersViewModel
            {
                UserList = um.GetAllUser()
            };
            return View(response);
        }

        public ActionResult CreateUser(CreateUserViewModel user)
        {
            Usuario usuário = new Usuario
            {
                Id = user.Id,
                IdSmart = user.IdSmart,
                Nome = user.Nome,
                eMail = user.Email,
                AreaDepartamento = user.AreaDepartamento,
                Gerente = user.Gerente,
                Funcao = user.Funcao,
                Ramal = user.Ramal
            };
            _um.AddUser(usuário);
            return View();
        }

    }
}