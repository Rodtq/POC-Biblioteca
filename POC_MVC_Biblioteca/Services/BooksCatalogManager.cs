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
using System.Net;
using System.IO;
using System.Security.Principal;

namespace POC_MVC_Biblioteca.Services
{
    public class BooksCatalogManager
    {

        public void AddBook(BooksViewModel book)
        {
            book.Status = 1;
            using (POC_Database db = new POC_Database())
            {
                Book parsedModel = ParseBookViewModelToBookModel(book);
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
                if (filters != null)
                {
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

        public byte[] ImageToByteParser(string imgInfo)
        {
            byte[] imageBytes = null;
            if (string.IsNullOrEmpty(imgInfo))
            {
                return imageBytes;
            }
            try
            {
                imageBytes = Convert.FromBase64String(imgInfo);
                return imageBytes;
            }
            catch (FormatException)
            {
                WebClient wc = new WebClient();
                ICredentials cred;
                cred = new NetworkCredential("Alexandre.Isabella", "GANZULETOVA");
                WebProxy wp = new WebProxy("sr-brz-dc01.smartm.internal", true, null, cred);
                wc.Proxy = wp;
                try
                {
                    imageBytes = wc.DownloadData(imgInfo);
                    return imageBytes;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }


        public void DeleteBook(int bookId)
        {
            using (POC_Database db = new POC_Database())
            {
                var bookEntity = db.Books.SingleOrDefault(b => b.Id == bookId);
                db.Books.Remove(bookEntity);
                db.SaveChanges();
            }
        }

        public BooksViewModel GetBookPerId(int bookId)
        {
            Book bookEntity = null;
            using (POC_Database db = new POC_Database())
            {
                bookEntity = db.Books.Include("Category").SingleOrDefault(b => b.Id == bookId);
            }
            BooksViewModel result = ParseBookModelToBookViewModel(bookEntity);
            return result;
        }



        public BooksViewModel UpdateBook(BooksViewModel book)
        {



            using (POC_Database db = new POC_Database())
            {
                Book parsedModel = ParseBookViewModelToBookModel(book);

                DbEntityEntry dbEntityEntry = db.Entry(parsedModel);

                if (dbEntityEntry.State == EntityState.Detached)
                {
                    db.Books.Attach(parsedModel);
                }

                dbEntityEntry.State = EntityState.Modified;
                db.SaveChanges();
                return book;
            }

        }

        private BooksViewModel ParseBookModelToBookViewModel(Book book)
        {
            BooksViewModel result = new BooksViewModel()
            {
                Author = book.Author,
                BookCategories = GetBookCategories(),
                BookCover = book.Cover != null ? Convert.ToBase64String(book.Cover) : null,
                BookYear = book.BookYear,
                CategoryId = book.Category.Id.ToString(),
                Description = book.Description,
                Editor = book.Editor,
                ISBN = book.ISBN,
                Id = book.Id,
                LocalizationShelf = book.LocalizationShelf,
                Observation = book.Observation,
                Quantity = book.Quantity,
                Title = book.Title
            };
            return result;
        }

        private Book ParseBookViewModelToBookModel(BooksViewModel book)
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
                    LocalizationShelf = book.LocalizationShelf,
                    Cover = ImageToByteParser(book.BookCover),
                    Id = book.Id,
                    Status = book.Status
                    
                };
                return parsedModel;
            }
        }
    }
}