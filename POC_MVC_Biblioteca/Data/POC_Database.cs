using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using POC_MVC_Biblioteca.Models;

namespace POC_MVC_Biblioteca.Data
{
    public class POC_Database : DbContext
    {

        public POC_Database(string databaseName = "POCDatabase")
            : base(databaseName)
        {
            
        }
        private DbSet<User> _users;
        private DbSet<Book> _booksCatalog;
        private DbSet<Role> _roles;
        private DbSet<Loan> _rents;

        public DbSet<User> Users
        {
            get
            {
                return _users;
            }

            set
            {
                _users = value;
            }
        }

        public DbSet<Book> BooksCatalog
        {
            get
            {
                return _booksCatalog;
            }

            set
            {
                _booksCatalog = value;
            }
        }

        public DbSet<Role> Roles
        {
            get
            {
                return _roles;
            }

            set
            {
                _roles = value;
            }
        }

        public DbSet<Loan> Rents
        {
            get
            {
                return _rents;
            }

            set
            {
                _rents = value;
            }
        }
    }
}