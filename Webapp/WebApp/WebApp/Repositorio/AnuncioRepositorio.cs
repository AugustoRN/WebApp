using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.ContextoBD;
using WebApp.Domain;

namespace WebApp.Repositorio
{
    public class AnuncioRepositorio : IRepositorio<Anuncio>
    {
        private Contexto contexto;

        public AnuncioRepositorio(Contexto contexto)
        {
            this.contexto = contexto;
        }

        public IEnumerable<Anuncio> GetAll()
        {
            return contexto.Anuncios.ToList();
        }

        public void Adiciona(Anuncio u)
        {
            contexto.Anuncios.Add(u);
            contexto.SaveChanges();

        }

        public IList<Anuncio> Lista()
        {
            return contexto.Anuncios.ToList();
        }
    }
}