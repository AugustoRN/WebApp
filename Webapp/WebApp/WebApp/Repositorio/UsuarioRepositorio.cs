using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApp.ContextoBD;
using WebApp.Domain;

namespace WebApp.Repositorio
{
    public class UsuarioRepositorio : IRepositorio<Usuario>
    {


        private Contexto contexto;

        public UsuarioRepositorio(Contexto contexto)
        {
            this.contexto = contexto;
        }
        public IEnumerable<Usuario> GetAll()
        {
            return contexto.Usuarios.ToList();
        }
    

        public void Adiciona(Usuario u)
        {
            contexto.Usuarios.Add(u);
            contexto.SaveChanges();
        }

        public IList<Usuario> Lista()
        {
            return contexto.Usuarios.ToList();
        }




        public Usuario BuscarPorId(int? id)
        {
            Usuario u = new Usuario();
            u = contexto.Usuarios.Find(id);
            return u;
        }

        public void Editar(Usuario t)
        {
            contexto.Entry(t).State = EntityState.Modified;
            contexto.SaveChanges();
        }

        public void Remover(Usuario t)
        {
            contexto.Usuarios.Remove(t);
            contexto.SaveChanges();
        }
    }
}