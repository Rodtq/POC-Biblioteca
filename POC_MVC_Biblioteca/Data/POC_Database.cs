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
        private DbSet<Usuario> _usuarios;
        private DbSet<Catalogacao> _catalogacao;
        private DbSet<Role> _roles;
        private DbSet<Locacao> _locacoes;

        public DbSet<Usuario> Usuarios
        {
            get
            {
                return _usuarios;
            }

            set
            {
                _usuarios = value;
            }
        }

        public DbSet<Catalogacao> Catalogacao
        {
            get
            {
                return _catalogacao;
            }

            set
            {
                _catalogacao = value;
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

        public DbSet<Locacao> Locacoes
        {
            get
            {
                return _locacoes;
            }

            set
            {
                _locacoes = value;
            }
        }
    }
}