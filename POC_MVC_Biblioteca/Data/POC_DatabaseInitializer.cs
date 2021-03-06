﻿using POC_MVC_Biblioteca.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace POC_MVC_Biblioteca.Data
{
    public class POC_DatabaseInitializer : CreateDatabaseIfNotExists<POC_Database>
    {
        protected override void Seed(POC_Database context)
        {



            #region Role
            context.Roles.Add(new Role
            {
                Name = "Administrator",
            });

            context.Roles.Add(new Role
            {
                Name = "User",
            });
            #endregion
            #region User
            context.Users.Add(new User
            {
                SamAccountName = "rodtq",
                Name = "usuário de desenvolvimento",
                AreaDepartament = "desenvolvimento",
                eMail = "rodtq@hotmail.com",
                ExtensionLine = "ramal",
                Function = "desenvolvedor",
                IdSmart = 000,
                Manager = "Desenvolvedor",
                Roles = new HashSet<Role>(context.Roles.Where(r => r.Name == "Administrator").ToList())
            });
            #endregion
            #region bookCategory

            List<string> categoriesName = new List<string>();
            categoriesName.Add("Auto Ajuda");
            categoriesName.Add("Biografia");
            categoriesName.Add("Ciência");
            categoriesName.Add("Culinária/Gastronomia");
            categoriesName.Add("Esoterismo");
            categoriesName.Add("Ficção");
            categoriesName.Add("Filosofia");
            categoriesName.Add("História");
            categoriesName.Add("Infantil");
            categoriesName.Add("Informática");
            categoriesName.Add("Literatura");
            categoriesName.Add("Música");
            categoriesName.Add("Negócios");
            categoriesName.Add("Poemas");
            categoriesName.Add("Policial");
            categoriesName.Add("Política");
            categoriesName.Add("Psicologia");
            categoriesName.Add("Quadrinhos");
            categoriesName.Add("Religião");
            categoriesName.Add("Romance");
            categoriesName.Add("Saúde");
            categoriesName.Add("Suspense");
            categoriesName.Add("Terror");
            categoriesName.Add("Treinamento");


            categoriesName.ForEach(name =>
            {
                context.BookCategory.Add(new BookCategory()
                {
                    Name = name
                });
            });


            #endregion

            base.Seed(context);

        }

    }
}