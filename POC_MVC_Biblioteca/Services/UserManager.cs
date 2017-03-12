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

        public void AddUser(Usuario user)
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
                    db.Usuarios.Add(user);
                }
                db.SaveChanges();
            }
        }

        public void DeleteUser(Usuario user)
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
                    db.Usuarios.Attach(user);
                    db.Usuarios.Remove(user);
                }
                db.SaveChanges();
            }
        }

        public void DeleteBySmartId(int Id)
        {
            Usuario user = new Usuario();
            using (POC_Database db = new POC_Database())
            {
                user = db.Usuarios.SingleOrDefault(u => u.IdSmart == Id);
                db.Usuarios.Remove(user);
                db.SaveChanges();
            }
        }


        public Usuario UpdateUser(Usuario user)
        {
            Usuario updatedUser = new Usuario();
            using (POC_Database db = new POC_Database())
            {
                DbEntityEntry dbEntityEntry = db.Entry(user);
                if (dbEntityEntry.State == EntityState.Detached)
                {
                    db.Usuarios.Attach(user);
                }
                dbEntityEntry.State = EntityState.Modified;
                db.SaveChanges();
            }
            updatedUser = user;
            return updatedUser;
        }

        public IEnumerable<Usuario> GetAllUser()
        {
            POC_Database db = new POC_Database();
            return db.Usuarios;
        }


        public Usuario GetById(int Id)
        {
            Usuario usuario = new Usuario();
            using (POC_Database db = new POC_Database())
            {
                usuario = db.Usuarios.SingleOrDefault(u => u.Id == Id);
            }
            return usuario;
        }


        public Usuario GetByIdSmart(int IdSmart)
        {
            Usuario usuario = new Usuario();
            using (POC_Database db = new POC_Database())
            {
                usuario = db.Usuarios.SingleOrDefault(u => u.IdSmart == IdSmart);
            }
            return usuario;
        }
    }
}