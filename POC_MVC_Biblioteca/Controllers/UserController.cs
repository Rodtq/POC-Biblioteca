using POC_MVC_Biblioteca.Models;
using POC_MVC_Biblioteca.Services;
using POC_MVC_Biblioteca.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
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
            user.Roles = _um.GetParsedRoles(Request.LogonUserIdentity);


            if (!ModelState.IsValid)
            {
                return PartialView("_UserRegister", user);
            }

            UserViewModel result = new UserViewModel()
            {
                Roles = _um.GetParsedRoles(Request.LogonUserIdentity)
            };
            bool isSuccess = _um.AddUser(user);
            if (!isSuccess)
            {
                ModelState.AddModelError("SamAccountName", "Usuário já existente!");
                result = user;
            }
            return PartialView("_UserRegister", result);
        }

        //[OutputCache(Duration = 10, VaryByParam = "*")]
        [NoCache]
        public ActionResult UserNavigation(string partialViewName)
        {
            MultiSelectList rolesItem = _um.GetParsedRoles(Request.LogonUserIdentity);



            switch (partialViewName)
            {
                case "_UserRegister":
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
        #region EditUser


        [NoCache]
        [HttpGet]
        public ActionResult GetUserToEdit(int userId)
        {
            UserViewModel result = new UserViewModel();
            result = _um.GetById(userId);
            TempData["roles"] = result.RolesId;
            result.Roles = _um.GetParsedRoles(Request.LogonUserIdentity);
            return PartialView("_UserEdit", result);
        }

        public ActionResult EditUser(UserViewModel user)
        {
            user.RolesId = TempData["roles"] as int[];
            if (!ModelState.IsValid)
            {
                return View(user);
            }
            _um.UpdateUser(user);
            user.Roles = _um.GetParsedRoles(Request.LogonUserIdentity);
            return PartialView("_UserEdit", user);
        }

        #endregion
        public ActionResult FindADUser(string samAccountName)
        {
            if (string.IsNullOrEmpty(samAccountName))
            {
                return PartialView("_UserRegister", new UserViewModel() { Roles = _um.GetParsedRoles(Request.LogonUserIdentity) });
            }
            UserViewModel model = _um.FindActiveDirectotyUser(samAccountName);
            model.Roles = _um.GetParsedRoles(Request.LogonUserIdentity);
            return PartialView("_UserRegister", model);
        }

        public ActionResult DeleteUser(int userId)
        {
            _um.DeleteById(userId);
            UserViewModel result = new UserViewModel();
            result.UserList = _um.GetUsers(result).UserList;
            return PartialView("_UserList", result);
        }
    }
}