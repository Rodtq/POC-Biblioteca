using POC_MVC_Biblioteca.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace POC_MVC_Biblioteca.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "DEIXE ABAIXO SUA SUGESTÃO";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "";

            return View();
        }

        public ActionResult SendSugestion(string opiniao)
        {
            string userName = HttpContext.User.Identity.Name;
            if (!string.IsNullOrEmpty(userName))
            {
                MailService sm = new MailService();
                sm.MailSender(userName, opiniao, "[SmartBooks] Nova Sugestão");
                TempData.Clear();
                return RedirectToAction("Index", "BooksCatalog");
            }
            else
            {
                var fullUrl = this.Url.Action("Contact", "Home", this.Request.Url.Scheme);
                TempData["returnUrl"] = fullUrl;
                TempData["SugestionMessage"] = opiniao;
                return RedirectToAction("Index", "Login");
            }
        }
    }
}