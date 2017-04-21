using POC_MVC_Biblioteca.Data;
using POC_MVC_Biblioteca.Models;
using POC_MVC_Biblioteca.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace POC_MVC_Biblioteca.Services
{
    public class UserManager
    {

        public void AddUser(UserViewModel user)
        {
            using (POC_Database db = new POC_Database())
            {

                User usr = new User
                {
                    Id = user.Id,
                    SamAccountName = user.SamAccountName,
                    IdSmart = user.IdSmart,
                    Name = user.Name,
                    eMail = user.Email,
                    AreaDepartament = user.AreaDepartament,
                    Manager = user.Manager,
                    Function = user.Function,
                    ExtensionLine = user.ExtensionLine,
                    Roles = SetRoles(user.RolesId),
                };


                try
                {
                    DbEntityEntry dbEntityEntry = db.Entry(usr);
                    if (dbEntityEntry.State != EntityState.Detached)
                    {
                        dbEntityEntry.State = EntityState.Added;
                    }
                    else
                    {
                        db.Users.Attach(usr);
                        db.Users.Add(usr);
                    }
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;

                }
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

        public UserViewModel GetUsers(UserViewModel filtros)
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
                    SamAccountName = u.SamAccountName,
                    Email = u.eMail,
                    AreaDepartament = u.AreaDepartament,
                    ExtensionLine = u.ExtensionLine,
                    Function = u.Function,
                    Id = u.Id,
                    IdSmart = u.IdSmart,
                    Manager = u.Manager,
                    Name = u.Name
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


        public User GetBySamAccountName(string SamAccountName)
        {
            User usuario = new User();
            using (POC_Database db = new POC_Database())
            {
                usuario = db.Users.SingleOrDefault(u => u.SamAccountName.Equals(SamAccountName));
            }
            return usuario;
        }


        public IEnumerable<Role> GetPrincipalRoles(string SamAccountName)
        {
            IEnumerable<Role> result = new List<Role>();
            using (POC_Database db = new POC_Database())
            {
                User user = db.Users.SingleOrDefault(u => u.SamAccountName == SamAccountName);
                result = user.Roles;
            }
            return result;
        }


        public IEnumerable<Role> GetRoles()
        {
            try
            {
                IEnumerable<Role> result = new List<Role>();
                IQueryable<Role> roles = null;
                using (POC_Database db = new POC_Database())
                {
                    roles = db.Roles;
                    result = roles.ToList();
                }
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public MultiSelectList GetParsedRoles()
        {
            try
            {
                MultiSelectList result = new MultiSelectList(GetRoles(), "Id", "Name");
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }



        private HashSet<Role> SetRoles(int[] RoleIds)
        {
            if (RoleIds == null)
            {
                return new HashSet<Role>(GetRoles().Where(r => r.Name.Equals("User")));
            }
            HashSet<Role> roles = new HashSet<Role>((from int rId in RoleIds
                                                     join Role role in GetRoles()
                                                     on rId equals role.Id
                                                     select role));
            return roles;
        }

    }
}