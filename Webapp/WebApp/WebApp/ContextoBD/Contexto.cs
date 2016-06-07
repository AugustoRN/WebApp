using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApp.Domain;

namespace WebApp.ContextoBD
{
    public class Contexto : DbContext
    {
        public DbSet<Anuncio> Anuncios { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
         
        }

    }
}