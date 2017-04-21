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
        public ActionResult Index(UserViewModel model)
        {
            UserViewModel result = new UserViewModel()
            {
                PartialName = model.PartialName,
                SamAccountName = model.SamAccountName

            };
            if (string.IsNullOrEmpty(result.PartialName))
            {
                result.PartialName = "_UserList";
            }
            return View(result);
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
            if (!ModelState.IsValid)
            {
                user.Roles = _um.GetParsedRoles();
                return PartialView("_UserRegister", user);
            }

            UserViewModel result = new UserViewModel()
            {
                Roles = _um.GetParsedRoles()
            };
            _um.AddUser(user);
            return PartialView("_UserRegister", result);
        }

        public ActionResult UserNavigation(string partialViewName)
        {
            switch (partialViewName)
            {
                case "_UserRegister":
                    MultiSelectList rolesItem = _um.GetParsedRoles();
                    if (rolesItem.Any())
                    {
                        return PartialView(partialViewName, new UserViewModel() { Roles = rolesItem });
                    }
                    return PartialView(partialViewName, new UserViewModel());
                case "_UserList":
                    UserViewModel response = new UserViewModel
                    {
                        UserList = _um.GetUsers(new UserViewModel()).UserList
                    };
                    return PartialView("_UserList", response);
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
            usr.Function = user.Function;
            usr.Id = usr.Id;
            usr.IdSmart = user.IdSmart;
            usr.Manager = user.Manager;
            usr.Name = user.Name;
            usr.Roles = null;
            _um.UpdateUser(usr);
            return PartialView("_UserEdit", user);
        }

    }
}