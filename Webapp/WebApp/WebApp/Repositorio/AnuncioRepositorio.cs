using System;
using System.Collections.Generic;
using System.Data.Entity;
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


        public Anuncio BuscarPorId(int? id)
        {
            Anuncio a = new Anuncio();
            a = contexto.Anuncios.Find(id);
            return a;
        }

        public void Editar(Anuncio t)
        {
            contexto.Entry(t).State = EntityState.Modified;
            contexto.SaveChanges();
        }

        public void Remover(Anuncio t)
        {
            contexto.Anuncios.Remove(t);
            contexto.SaveChanges();
        }
    }
}