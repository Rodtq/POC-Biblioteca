using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using POC_MVC_Biblioteca.Models;
using POC_MVC_Biblioteca.Data;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using POC_MVC_Biblioteca.ViewModels;
using System.Web.Mvc;

namespace POC_MVC_Biblioteca.Services
{
    public class BooksCatalogManager
    {

        public void AddBook(BooksViewModel book)
        {
            using (POC_Database db = new POC_Database())
            {
                int categoryId = Convert.ToInt32(book.CategoryId);
                Book parsedModel = new Book()
                {
                    ISBN = book.ISBN,
                    Title = book.Title,
                    Author = book.Author,
                    BookYear = book.BookYear,
                    Category = db.BookCategory.SingleOrDefault(b => b.Id == categoryId),
                    Editor = book.Editor,
                    Quantity = book.Quantity,
                    Description = book.Description,
                    Observation = book.Observation,
                    LocalizationShelf = book.LocalizationShelf
                };
                DbEntityEntry dbEntityEntry = db.Entry(parsedModel);
                if (dbEntityEntry.State != EntityState.Detached)
                {
                    dbEntityEntry.State = EntityState.Added;
                }
                else
                {
                    db.Books.Attach(parsedModel);
                    db.Books.Add(parsedModel);
                }
                db.SaveChanges();
            }
        }

        public IEnumerable<Book> GetBooks(BooksViewModel filters = null)
        {
            IEnumerable<Book> result = new List<Book>();
            IQueryable<Book> query = null;
            using (POC_Database db = new POC_Database())
            {
                query = db.Books.Include("Category");
                if (!string.IsNullOrEmpty(filters.Author))
                {
                    query = query.Where(l => l.Author.Contains(filters.Author));
                }
                if (!string.IsNullOrEmpty(filters.CategoryId))
                {
                    query = query.Where(l => l.Category.Id == Convert.ToInt32(filters.CategoryId));
                }
                if (!string.IsNullOrEmpty(filters.Editor))
                {
                    query = query.Where(l => l.Editor.Contains(filters.Editor));
                }
                if (!string.IsNullOrEmpty(filters.Title))
                {
                    query = query.Where(l => l.Title.Contains(filters.Title));
                }
                result = query.ToList();
            }
            return result;
        }

        public IEnumerable<SelectListItem> GetBookCategories()
        {
            IEnumerable<SelectListItem> result = new List<SelectListItem>();
            IQueryable<BookCategory> query = null;
            using (POC_Database db = new POC_Database())
            {
                query = db.BookCategory;
                result = query.ToList().Select(bc => new SelectListItem { Value = bc.Id.ToString(), Text = bc.Name });
            }
            return result;
        }
    }
}