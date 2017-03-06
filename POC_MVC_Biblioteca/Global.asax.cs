using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using POC_MVC_Biblioteca.Data;
using System.Data.Entity;

namespace POC_MVC_Biblioteca
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            using (var db = new POC_Database())
            {
                Database.SetInitializer(new POC_DatabaseInitializer());
                db.Database.Initialize(false);
            }
        }
    }
}
