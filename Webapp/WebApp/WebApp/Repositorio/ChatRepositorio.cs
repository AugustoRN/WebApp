using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApp.ContextoBD;
using WebApp.Domain;

namespace WebApp.Repositorio
{
    public class ChatRepositorio : IRepositorio<Chat>
    {

         private Contexto contexto;

        public ChatRepositorio(Contexto contexto)
        {
            this.contexto = contexto;
        }
        public IEnumerable<Chat> GetAll()
        {
            return contexto.Chats.ToList();    
        }

        public void Adiciona(Chat u)
        {
            contexto.Chats.Add(u);
            contexto.SaveChanges();
        }

        public IList<Chat> Lista()
        {
           return  contexto.Chats.ToList();
        }

        public Chat BuscarPorId(int? id)
        {
            Chat a = new Chat();
            a = contexto.Chats.Find(id);
            return a;
        }

        public void Editar(Chat t)
        {
            contexto.Entry(t).State = EntityState.Modified;
            contexto.SaveChanges();
        }

        public void Remover(Chat t)
        {
            contexto.Chats.Remove(t);
            contexto.SaveChanges();
        }
    }
}