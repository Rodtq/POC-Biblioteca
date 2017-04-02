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
    public class UserController : Controller
    {
        private readonly UserManager _um;
        public UserController()
        {
            _um = new UserManager();
        }
        
        // GET: Usuario
        public ActionResult Index()
        {
            UserManager um = new UserManager();
            AllUsersViewModel response = new AllUsersViewModel
            {
                UserList = um.GetAllUsers()
            };
            return View();
        }

        public ActionResult GetAllUsers()
        {
            UserManager um = new UserManager();
            AllUsersViewModel response = new AllUsersViewModel
            {
                UserList = um.GetAllUsers()
            };
            return PartialView("_UserList",response);
        }

        public ActionResult CreateUser(CreateUserViewModel user)
        {
            User usuário = new User
            {
                Id = user.Id,
                IdSmart = user.IdSmart,
                Name = user.Nome,
                eMail = user.Email,
                AreaDepartament = user.AreaDepartamento,
                Manager = user.Gerente,
                Function = user.Funcao,
                ExtensionLine = user.Ramal
            };
            
            _um.AddUser(usuário);
            return RedirectToAction("Index");
        }

        public ActionResult UserNavigation(string partialViewName)
        {
            switch (partialViewName)
            {
                case "_UserRegister":
                    return PartialView(partialViewName, new CreateUserViewModel());
                case "_UserList":
                    return RedirectToAction("GetAllUsers");
                case "_UserEdit":
                    return PartialView(partialViewName);
                case "_UserDelete":
                    return PartialView(partialViewName);
                default:
                    return null;
            }

        }

    }
}