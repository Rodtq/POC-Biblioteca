using POC_MVC_Biblioteca.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace POC_MVC_Biblioteca.Data
{
    public class POC_DatabaseInitializer : CreateDatabaseIfNotExists<POC_Database>
    {
        protected override void Seed(POC_Database context)
        {

            base.Seed(context);

        }

    }
}