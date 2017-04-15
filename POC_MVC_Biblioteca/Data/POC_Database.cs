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
        private DbSet<Book> _books;
        private DbSet<Role> _roles;
        private DbSet<Loan> _loan;
        private DbSet<BookCategory> _bookCategory;
        private DbSet<Reserve> _reserve;

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

        public DbSet<Book> Books
        {
            get
            {
                return _books;
            }

            set
            {
                _books = value;
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

        public DbSet<Loan> Loan
        {
            get
            {
                return _loan;
            }

            set
            {
                _loan = value;
            }
        }
        public DbSet<BookCategory> BookCategory
        {
            get
            {
                return _bookCategory;
            }
            set
            {
                _bookCategory = value;
            }
        }

        public DbSet<Reserve> Reserve
        {
            get
            {
                return _reserve;
            }

            set
            {
                _reserve = value;
            }
        }
    }
}