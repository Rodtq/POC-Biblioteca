﻿using POC_MVC_Biblioteca.Data;
using POC_MVC_Biblioteca.Models;
using POC_MVC_Biblioteca.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.DirectoryServices.AccountManagement;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace POC_MVC_Biblioteca.Services
{
    public class UserManager
    {

        public bool AddUser(UserViewModel user)
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
                    return true;
                }
                catch (DbUpdateException ex)
                {
                    return false;
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


        public UserViewModel UpdateUser(UserViewModel user)
        {
            User usr = new User();
            usr.AreaDepartament = user.AreaDepartament;
            usr.eMail = user.Email;
            usr.ExtensionLine = user.ExtensionLine;
            usr.Function = user.Function;
            usr.Id = user.Id;
            usr.IdSmart = user.IdSmart;
            usr.Manager = user.Manager;
            usr.Name = user.Name;
            usr.Roles = SetRoles(user.RolesId);
            usr.SamAccountName = user.SamAccountName;
            using (POC_Database db = new POC_Database())
            {
                DbEntityEntry dbEntityEntry = db.Entry(usr);
                if (dbEntityEntry.State == EntityState.Detached)
                {
                    db.Users.Attach(usr);
                }
                dbEntityEntry.State = EntityState.Modified;
                db.SaveChanges();
            }
            return user;
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
                    Name = u.Name,
                });
            }
            return Result;
        }


        public UserViewModel GetById(int Id)
        {
            User usuario = new User();
            using (POC_Database db = new POC_Database())
            {
                usuario = db.Users.SingleOrDefault(u => u.Id == Id);
                UserViewModel result = new UserViewModel
                {
                    IdSmart = usuario.IdSmart,
                    AreaDepartament = usuario.AreaDepartament,
                    Email = usuario.eMail,
                    ExtensionLine = usuario.ExtensionLine,
                    Function = usuario.Function,
                    Id = usuario.Id,
                    Manager = usuario.Manager,
                    Name = usuario.Name,
                    SamAccountName = usuario.SamAccountName,
                    RolesId = usuario.Roles.Select(u=>u.Id).ToArray() 
                };
                return result;
            }
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


        public MultiSelectList GetParsedRoles(System.Security.Principal.WindowsIdentity userIdentity)
        {
            var roles = GetRoles();

            bool isAuth = userIdentity.IsAuthenticated;
            // Cast the Thread.CurrentPrincipal
            if (isAuth)
            {
                ClaimsPrincipal icp = Thread.CurrentPrincipal as ClaimsPrincipal;
                // Access IClaimsIdentity which contains claims
                ClaimsIdentity claimsIdentity = (ClaimsIdentity)icp.Identity;
                var isAdmin = claimsIdentity.Claims.Any(r => r.Value.Equals("Administrator"));
                if (!isAdmin)
                {
                    roles = roles.Except(roles.Where(r => r.Id == 1));
                }
            }
            else
            {
                roles = roles.Except(roles.Where(r => r.Id == 1));
            }

            try
            {
                MultiSelectList result = new MultiSelectList(roles, "Id", "Name");
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


        public UserViewModel FindActiveDirectotyUser(string samAccountName)
        {
            var user = new UserViewModel() { SamAccountName = samAccountName };

            var foundDC = DomainController.FindOne(new DirectoryContext(DirectoryContextType.Domain),
             ActiveDirectorySite.GetComputerSite().ToString(),
             LocatorOptions.ForceRediscovery | LocatorOptions.WriteableRequired);

            var searcher = foundDC.GetDirectorySearcher();
            searcher.Filter = string.Format("(&(objectClass=user)(objectCategory=person)(samAccountName={0}))", samAccountName);
            //var root = new DirectoryEntry("LDAP://" + Properties.Settings.Default.ActiveDirectoryPath);
            //var searcher = new DirectorySearcher(root)
            //{
            //    Filter =
            //        string.Format("(&(objectClass=user)(objectCategory=person)(samAccountName={0}))", samAccountName)
            //};
            searcher.PropertiesToLoad.Add("name");
            searcher.PropertiesToLoad.Add("mail");
            searcher.PropertiesToLoad.Add("manager");
            searcher.PropertiesToLoad.Add("telephoneNumber");
            searcher.PropertiesToLoad.Add("department");
            searcher.PropertiesToLoad.Add("samAccountName");
            searcher.PropertiesToLoad.Add("thumbnailPhoto");
            searcher.PropertiesToLoad.Add("employeeID");

            var result = searcher.FindOne();
            if (result != null)
            {
                if (result.Properties.Contains("samAccountName"))
                {
                    user.SamAccountName = (string)result.Properties["samAccountName"][0];
                }
                if (result.Properties.Contains("name"))
                {
                    user.Name = (string)result.Properties["name"][0];
                }
                if (result.Properties.Contains("mail"))
                {
                    user.Email = (string)result.Properties["mail"][0];
                }
                if (result.Properties.Contains("manager"))
                {
                    searcher.Filter = string.Format("(&(objectClass=user)(objectCategory=person)(distinguishedName={0}))", (string)result.Properties["manager"][0]);
                    searcher.PropertiesToLoad.Add("name");
                    var managerResult = searcher.FindOne();
                    if (managerResult != null)
                    {
                        user.Manager = (string)managerResult.Properties["name"][0];
                    }
                }
                if (result.Properties.Contains("telephoneNumber"))
                {
                    user.ExtensionLine = (string)result.Properties["telephoneNumber"][0];
                }
                if (result.Properties.Contains("department"))
                {
                    user.AreaDepartament = (string)result.Properties["department"][0];
                }
                if (result.Properties.Contains("samAccountName"))
                {
                    user.SamAccountName = (string)result.Properties["samAccountName"][0];
                }
                if (result.Properties.Contains("thumbnailPhoto"))
                {
                    user.Photo = (byte[])result.Properties["thumbnailPhoto"][0];
                }
                if (result.Properties.Contains("employeeID"))
                {
                    string fullId = result.Properties["employeeID"][0].ToString();
                    string[] splittedId = fullId.Split('-');
                    if (splittedId.Length > 0)
                    {
                        int n;
                        for (int i = 0; i < splittedId.Length; i++)
                        {
                            bool isNumeric = int.TryParse(splittedId[i], out n);
                            if (isNumeric)
                            {
                                int numberId = Convert.ToInt32(splittedId[i]);
                                user.IdSmart = numberId;
                                break;
                            }
                        }
                    }
                }

                return user;
            }
            return null;
        }

        private bool IsLocalUser(string accountName)
        {
            var domainContext = new PrincipalContext(ContextType.Machine);
            return Principal.FindByIdentity(domainContext, accountName) != null;
        }
    }
}