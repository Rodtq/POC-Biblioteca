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
            return View();
        }

        public ActionResult GetUsers(UserViewModel filtros)
        {

            UserViewModel response = new UserViewModel
            {
                UserList = _um.GetUsers(filtros).UserList
            };
            return PartialView("_UserList", response);
        }

        public ActionResult CreateUser(UserViewModel user)
        {
            User usuário = new User
            {
                Id = user.Id,
                IdSmart = user.IdSmart,
                Name = user.Name,
                eMail = user.Email,
                AreaDepartament = user.AreaDepartament,
                Manager = user.Manager,
                Function = user.Funtion,
                ExtensionLine = user.ExtensionLine,
                Roles = SetRoles(user.RolesId),
            };
            UserViewModel result = new UserViewModel()
            {
                Roles = GetRoles(),
            };
            _um.AddUser(usuário);
            return PartialView("_UserRegister", result);
        }

        public ActionResult UserNavigation(string partialViewName)
        {
            switch (partialViewName)
            {
                case "_UserRegister":
                    MultiSelectList rolesItem = GetRoles();
                    if (rolesItem.Any())
                    {
                        return PartialView(partialViewName, new UserViewModel() { Roles = rolesItem });
                    }
                    return PartialView(partialViewName, new UserViewModel());
                case "_UserList":
                    return RedirectToAction("GetUsers");
                case "_UserEdit":
                    return PartialView(partialViewName);
                case "_UserDelete":
                    return PartialView(partialViewName);
                default:
                    return null;
            }
        }

        public ActionResult EditUser(UserViewModel user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }
            User usr = new User();
            usr.AreaDepartament = user.AreaDepartament;
            usr.eMail = user.Email;
            usr.ExtensionLine = user.ExtensionLine;
            usr.Function = user.Funtion;
            usr.Id = usr.Id;
            usr.IdSmart = user.IdSmart;
            usr.Manager = user.Manager;
            usr.Name = user.Name;
            usr.Roles = null;
            _um.UpdateUser(usr);
            return PartialView("_UserEdit", user);
        }


        private HashSet<Role> SetRoles(int[] RoleIds)
        {

            HashSet<Role> roles = new HashSet<Role>((from int rId in RoleIds
                                                     join Role role in _um.GetRoles()
                                                     on rId equals role.Id
                                                     select role));
            return roles;
        }

        private MultiSelectList GetRoles()
        {
            MultiSelectList result = new MultiSelectList(_um.GetRoles(), "Id", "Name");
            return result;
        }
    }
}