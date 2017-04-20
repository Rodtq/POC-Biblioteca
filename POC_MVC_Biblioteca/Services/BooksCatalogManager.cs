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
                    db.Books.Attach(catalogacao);
                    db.Books.Add(catalogacao);
                }
                db.SaveChanges();
            }
        }

        public IEnumerable<Book> GetBooks(BooksConsultViewModel filtros = null)
        {
            IEnumerable<Book> Result = new List<Book>();
            IQueryable<Book> Query = null;
            using (POC_Database db = new POC_Database())
            {
                Query = db.Books.Include("Category");
                if (!string.IsNullOrEmpty(filtros.AuthorFilter))
                {
                    Query = Query.Where(l => l.Author.Contains(filtros.AuthorFilter));
                }
                if (!string.IsNullOrEmpty(filtros.CategroryFilter))
                {
                    //Query = Query.Where(l => l.Category.Contains(filtros.CategroryFilter));
                }
                if (!string.IsNullOrEmpty(filtros.EditorFilter))
                {
                    Query = Query.Where(l => l.Editor.Contains(filtros.EditorFilter));
                }
                if (!string.IsNullOrEmpty(filtros.TitleFilter))
                {
                    Query = Query.Where(l => l.Title.Contains(filtros.TitleFilter));
                }
                Result = Query.ToList();
            }
            return Result;
        }

        public IEnumerable<BookCategory> GetBookCategories()
        {
            IEnumerable<BookCategory> result = new List<BookCategory>();
            using (POC_Database db = new POC_Database())
            {
                result = db.BookCategory.ToList();
            }
            return result;
        }

    }
}