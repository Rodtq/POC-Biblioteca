using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using POC_MVC_Biblioteca.Models;
using POC_MVC_Biblioteca.Data;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using POC_MVC_Biblioteca.ViewModels;

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
                    db.Catalogacao.Add(catalogacao);
                }
                db.SaveChanges();
            }
        }

        public IEnumerable<Catalogacao> GetCatalogacao(ConsultaLivroViewModel filtros =null)
        {
            IEnumerable<Catalogacao> result = null;
            using (POC_Database db = new POC_Database())
            {
                result = db.Catalogacao.ToList();
                if (filtros.FiltroAutor !=null && !filtros.FiltroAutor.Equals(""))
                {
                    result = result.Where(l => l.Autor.ToLowerInvariant().Contains(filtros.FiltroAutor.ToLowerInvariant()));
                }
                if (filtros.FiltroCategoria != null && !filtros.FiltroCategoria.Equals(""))
                {
                    result = result.Where(l => l.Categoria.ToLowerInvariant().Contains(filtros.FiltroCategoria.ToLowerInvariant()));
                }
                if (filtros.FiltroEditora != null && !filtros.FiltroEditora.Equals(""))
                {
                    result = result.Where(l => l.Editora.ToLowerInvariant().Contains(filtros.FiltroEditora.ToLowerInvariant()));
                }
                if (filtros.FiltroTitulo != null && !filtros.FiltroTitulo.Equals(""))
                {
                    result = result.Where(l => l.TituloDaObra.ToLowerInvariant().Contains(filtros.FiltroTitulo.ToLowerInvariant()));
                }
            }
            return result;
        }
    }
}