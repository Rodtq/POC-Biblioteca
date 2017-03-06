using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace POC_MVC_Biblioteca.Controllers
{
    public class AcervoController : Controller
    {
        // GET: Acervo
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult CadastrarLivros()
        {
            return PartialView("");
        }
    }
}