using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using POC_MVC_Biblioteca.Services;
using POC_MVC_Biblioteca.ViewModels;
using Microsoft.Owin.Security;

namespace POC_MVC_Biblioteca.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Index(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            IAuthenticationManager authenticationManager = HttpContext.GetOwinContext().Authentication;
            var authService = new LoginService(authenticationManager);

            UserManager um = new UserManager();
            if (um.GetBySamAccountName(model.UserName) == null)
            {
                UserViewModel modelTo = new UserViewModel()
                {
                    PartialName = "_UserRegister",
                    SamAccountName = model.UserName
                };

                return RedirectToAction("Index", "User", modelTo);
            }

            var authenticationResult = authService.SignIn(model.UserName, model.Password);
            if (authenticationResult.IsSuccess)
            {
                return RedirectToLocal(returnUrl);
            }
            ModelState.AddModelError("", authenticationResult.ErrorMessage);
            return View(model);

        }
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "BooksCatalog");
        }
        [ValidateAntiForgeryToken]
        public virtual ActionResult Logoff()
        {
            IAuthenticationManager authenticationManager = HttpContext.GetOwinContext().Authentication;
            authenticationManager.SignOut(App_Start.CustomAuthentication.ApplicationCookie);

            return RedirectToAction("Index");
        }
    }
}