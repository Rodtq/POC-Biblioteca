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

        public bool SendSugestion(string opiniao)
        {
            string userName = HttpContext.User.Identity.Name;
            MailService sm = new MailService();
            return sm.MailSender(userName, opiniao, "[SmartBooks] Nova Sugestão");
        }
    }
}