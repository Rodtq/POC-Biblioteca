using POC_MVC_Biblioteca.Data;
using POC_MVC_Biblioteca.Models;
using POC_MVC_Biblioteca.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace POC_MVC_Biblioteca.Services
{
    public class UserManager
    {

        public void AddUser(User user)
        {
            using (POC_Database db = new POC_Database())
            {
                DbEntityEntry dbEntityEntry = db.Entry(user);
                if (dbEntityEntry.State != EntityState.Detached)
                {
                    dbEntityEntry.State = EntityState.Added;
                }
                else
                {
                    db.Users.Add(user);
                }
                db.SaveChanges();
            }
        }

        public void DeleteUser(User user)
        {
            using (POC_Database db = new POC_Database())
            {
                DbEntityEntry dbEntityEntry = db.Entry(user);
                if (dbEntityEntry.State != EntityState.Deleted)
                {
                    dbEntityEntry.State = EntityState.Deleted;
                }
                else
                {
                    db.Users.Attach(user);
                    db.Users.Remove(user);
                }
                db.SaveChanges();
            }
        }

        public void DeleteBySmartId(int Id)
        {
            User user = new User();
            using (POC_Database db = new POC_Database())
            {
                user = db.Users.SingleOrDefault(u => u.IdSmart == Id);
                db.Users.Remove(user);
                db.SaveChanges();
            }
        }


        public User UpdateUser(User user)
        {
            User updatedUser = new User();
            using (POC_Database db = new POC_Database())
            {
                DbEntityEntry dbEntityEntry = db.Entry(user);
                if (dbEntityEntry.State == EntityState.Detached)
                {
                    db.Users.Attach(user);
                }
                dbEntityEntry.State = EntityState.Modified;
                db.SaveChanges();
            }
            updatedUser = user;
            return updatedUser;
        }

        public UserViewModel GetAllUsers(UserViewModel filtros)
        {
            UserViewModel Result = new UserViewModel();
            IQueryable<User> Query = null;
            using (POC_Database db = new POC_Database())
            {
                Query = db.Users;
                if (filtros.IdSmart > 0)
                {
                    Query = Query.Where(l => l.IdSmart == filtros.IdSmart);
                }
                if (!string.IsNullOrEmpty(filtros.Name))
                {
                    Query = Query.Where(l => l.Name.Contains(filtros.Name));
                }
                if (!string.IsNullOrEmpty(filtros.AreaDepartament))
                {
                    Query = Query.Where(l => l.AreaDepartament.Contains(filtros.AreaDepartament));
                }
                Result.UserList = Query.ToList().Select(u => new UserViewModel()
                {
                    Email = u.eMail,
                    AreaDepartament = u.AreaDepartament,
                    ExtensionLine = u.ExtensionLine,
                    Funtion = u.Function,
                    Id = u.Id,
                    IdSmart = u.IdSmart,
                    Manager = u.Manager,
                    Name = u.Manager
                });
            }
            return Result;
        }


        public User GetById(int Id)
        {
            User usuario = new User();
            using (POC_Database db = new POC_Database())
            {
                usuario = db.Users.SingleOrDefault(u => u.Id == Id);
            }
            return usuario;
        }


        public User GetByIdSmart(int IdSmart)
        {
            User usuario = new User();
            using (POC_Database db = new POC_Database())
            {
                usuario = db.Users.SingleOrDefault(u => u.IdSmart == IdSmart);
            }
            return usuario;
        }
    }
}