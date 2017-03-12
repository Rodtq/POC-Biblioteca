using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using POC_MVC_Biblioteca.Models;
using POC_MVC_Biblioteca.Data;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;

namespace POC_MVC_Biblioteca.Services
{
    public class AcervoManager
    {

        public void AddCatalogacao(Catalogacao catalogacao)
        {
            using (POC_Database db = new POC_Database())
            {
                DbEntityEntry dbEntityEntry = db.Entry(catalogacao);
                if (dbEntityEntry.State != EntityState.Detached)
                {
                    dbEntityEntry.State = EntityState.Added;
                }
                else
                {
                    db.Catalogacao.Add(catalogacao)
                }
                db.SaveChanges();
            }
        }
    }
}