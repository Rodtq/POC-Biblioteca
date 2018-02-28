using POC_MVC_Biblioteca.Data;
using POC_MVC_Biblioteca.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace POC_MVC_Biblioteca.Services
{
    public class ReserveManager
    {
        public bool BookReserver(int bookId, string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return false;
            }
            User userModel = null;
            Book bookModel = null;
            Reserve reseve = null;
            using (POC_Database db = new POC_Database())
            {
                bookModel = db.Books.FirstOrDefault(b => b.Id == bookId);
                userModel = db.Users.FirstOrDefault(u => string.Compare(u.SamAccountName, username, StringComparison.OrdinalIgnoreCase) == 0);

                bool IsReserved = db.Reserve.Any(b => b.Id_Book == bookId && b.Id_User == userModel.Id);
                if (IsReserved)
                {
                    return false;
                }

                reseve = new Reserve()
                {
                    Id_Book = bookModel.Id,
                    Id_User = userModel.Id,
                    Book = bookModel,
                    User = userModel
                };

                DbEntityEntry dbEntityEntry = db.Entry(reseve);
                if (dbEntityEntry.State != EntityState.Detached)
                {
                    dbEntityEntry.State = EntityState.Added;
                }
                else
                {
                    db.Reserve.Attach(reseve);
                    db.Reserve.Add(reseve);
                }
                dbEntityEntry = db.Entry(bookModel);
                dbEntityEntry.State = EntityState.Modified;
                db.SaveChanges();
            }
            return true;
        }
    }
}