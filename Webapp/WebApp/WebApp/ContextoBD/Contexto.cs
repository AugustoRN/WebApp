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
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Bomba> Bombas { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Anuncio>().HasRequired(a => a.Usuario);
            
        }

    }
}