﻿using System;
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
            ViewBag.Message = "BIBLIOTECA SMART.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "BIBLIOTECA SMART.";

            return View();
        }
    }
}