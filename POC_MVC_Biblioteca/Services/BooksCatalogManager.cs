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
    public class BooksCatalogManager
    {

        public void AddBook(Book catalogacao)
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
                    db.BooksCatalog.Add(catalogacao);
                }
                db.SaveChanges();
            }
        }

        public IEnumerable<Book> GetBooks(BooksConsultViewModel filtros =null)
        {
            IEnumerable<Book> result = null;
            using (POC_Database db = new POC_Database())
            {
                result = db.BooksCatalog.ToList();
                if (filtros.AuthorFilter !=null && !filtros.AuthorFilter.Equals(""))
                {
                    result = result.Where(l => l.Author.ToLowerInvariant().Contains(filtros.AuthorFilter.ToLowerInvariant()));
                }
                if (filtros.CategroryFilter != null && !filtros.CategroryFilter.Equals(""))
                {
                    result = result.Where(l => l.Category.ToLowerInvariant().Contains(filtros.CategroryFilter.ToLowerInvariant()));
                }
                if (filtros.EditorFilter != null && !filtros.EditorFilter.Equals(""))
                {
                    result = result.Where(l => l.Editor.ToLowerInvariant().Contains(filtros.EditorFilter.ToLowerInvariant()));
                }
                if (filtros.TitleFilter != null && !filtros.TitleFilter.Equals(""))
                {
                    result = result.Where(l => l.Title.ToLowerInvariant().Contains(filtros.TitleFilter.ToLowerInvariant()));
                }
            }
            return result;
        }
    }
}