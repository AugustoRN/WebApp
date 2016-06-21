using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApp.ContextoBD;
using WebApp.Domain;

namespace WebApp.Repositorio
{
    public class BombaRepositorio : IRepositorio<Bomba>
    {
        private Contexto contexto;

        public BombaRepositorio(Contexto contexto)
        {
            this.contexto = contexto;
        }

        public IEnumerable<Bomba> GetAll()
        {
            return contexto.Bombas.ToList();
        }

        public void Adiciona(Bomba u)
        {
            contexto.Bombas.Add(u);
            contexto.SaveChanges();
        }

        public IList<Bomba> Lista()
        {
            return contexto.Bombas.ToList();
        }

        public Bomba BuscarPorId(int? id)
        {
            Bomba a = new Bomba();
            a = contexto.Bombas.Find(id);
            return a;
        }

        public void Editar(Bomba t)
        {
            contexto.Entry(t).State = EntityState.Modified;
            contexto.SaveChanges();
        }

        public void Remover(Bomba t)
        {
            contexto.Bombas.Remove(t);
            contexto.SaveChanges();
        }
    }
}