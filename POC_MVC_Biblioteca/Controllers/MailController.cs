using POC_MVC_Biblioteca.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace POC_MVC_Biblioteca.Controllers
{
    public class MailController : Controller
    {
        // GET: Mail
        public ActionResult Index()
        {
            MailService mservice = new MailService();
            mservice.CheckForLateDeliveries();
            return null;
        }
    }
}