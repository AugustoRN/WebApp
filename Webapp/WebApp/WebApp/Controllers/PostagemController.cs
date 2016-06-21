using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApp.ContextoBD;
using WebApp.Domain;
using WebApp.Repositorio;
using PagedList;
using System.IO;
using WebMatrix.WebData;
using WebApp.Models;
using System.Collections;
namespace WebApp.Controllers
{
    public class PostagemController : Controller
    {
        private AnuncioRepositorio anuncioDAO;
        private UsuarioRepositorio usuarioDAO;
        private ChatRepositorio chatDAO;
        private BombaRepositorio bombaDAO;

        public PostagemController(AnuncioRepositorio anuncioDAO, UsuarioRepositorio usuarioDAO, ChatRepositorio chatDAO,BombaRepositorio bombaDAO)
        {
            this.anuncioDAO = anuncioDAO;
            this.usuarioDAO = usuarioDAO;
            this.chatDAO = chatDAO;
            this.bombaDAO = bombaDAO;
        }

        public ActionResult Anunciar(int? pagina)
        {
            int tamanhoPagina = 2;
            int numeroPagina = pagina ?? 1;
            
            return View(anuncioDAO.GetAll().ToPagedList(numeroPagina, tamanhoPagina));
        }
        
      

        // GET: Postagem/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Anuncio anuncio = anuncioDAO.BuscarPorId(id);

            IList<ChatModel> listaDeChats = new List<ChatModel>();
            foreach(Chat novo in chatDAO.Lista() ){
                int usaurioId = novo.UsuarioId;
                int anuncioId = novo.AnuncioId;
                Usuario u = usuarioDAO.BuscarPorId(usaurioId);
                Anuncio a = anuncioDAO.BuscarPorId(anuncioId);
                ChatModel c = new ChatModel();
                c.Anuncio = a;
                c.AnuncioId = anuncioId;
                c.UsuarioId = usaurioId;
                c.Usuario = u;
                c.Texto = novo.Texto;
                if (id == anuncioId)
                {
                    listaDeChats.Add(c);
                }
               
            }
            ViewBag.Chats = listaDeChats;
            if (anuncio == null)
            {
                return HttpNotFound();
            }

            return View(anuncio);
        }

        // GET: Postagem/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.Usuarios = usuarioDAO.Lista();
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Anuncio anuncio,HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                if (upload != null && upload.ContentLength > 0)
                {
                    var arqImagem = new Anuncio
                    {
                        ImagemTipo = upload.ContentType
                    };
                    using(var reader = new BinaryReader(upload.InputStream))
                    {
                        arqImagem.Imagem = reader.ReadBytes(upload.ContentLength);
                    }
                    anuncio.Imagem = arqImagem.Imagem;
                    anuncio.ImagemTipo = arqImagem.ImagemTipo;
                }
                Usuario u = new Usuario();
               
                int mu1 = (int)WebSecurity.CurrentUserId;
                u = usuarioDAO.BuscarPorId(mu1);

                anuncio.UsuarioId = mu1;
                anuncio.Usuario = u;
                    
                   
                anuncioDAO.Adiciona(anuncio);
               
                TempData["mensagem"] = string.Format("{0}: foi incluido com sucesso", anuncio.Titulo);
                return RedirectToAction("Anunciar");
            }
           
            return View(anuncio);
        }

        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Anuncio anuncio = anuncioDAO.BuscarPorId(id);
            if (anuncio == null)
            {
                return HttpNotFound();
            }
            ViewBag.Usuarios = usuarioDAO.Lista();
            return View(anuncio);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Anuncio anuncio,HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                if (upload != null && upload.ContentLength > 0)
                {
                    var arqImagem = new Anuncio
                    {
                        ImagemTipo = upload.ContentType
                    };
                    using (var reader = new BinaryReader(upload.InputStream))
                    {
                        arqImagem.Imagem = reader.ReadBytes(upload.ContentLength);
                    }
                    anuncio.Imagem = arqImagem.Imagem;
                    anuncio.ImagemTipo = arqImagem.ImagemTipo;
                }
                Usuario u = new Usuario();

                int mu1 = (int)WebSecurity.CurrentUserId;
                u = usuarioDAO.BuscarPorId(mu1);

                anuncio.UsuarioId = mu1;
                anuncio.Usuario = u;
                anuncioDAO.Editar(anuncio);
                TempData["mensagem"] = string.Format("{0}: foi Editado com sucesso", anuncio.Titulo);
                return RedirectToAction("Anunciar");
            }

            return View(anuncio);
        }

        // GET: Postagem/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Anuncio anuncio = anuncioDAO.BuscarPorId(id);
            if (anuncio == null)
            {
                return HttpNotFound();
            }
            return View(anuncio);
        }

        // POST: Postagem/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Anuncio anuncio = anuncioDAO.BuscarPorId(id);
            anuncioDAO.Remover(anuncio);
            TempData["mensagem"] = string.Format("{0}: foi Deletado com sucesso", anuncio.Titulo);
            return RedirectToAction("Anunciar");
        }

        public ActionResult Comenta(String comentario, int anuncio)
        {
            if (!comentario.Equals(""))
            {
                Chat c = new Chat();
                c.AnuncioId = anuncio;
                c.UsuarioId = WebSecurity.CurrentUserId;
                c.Texto = comentario;

                chatDAO.Adiciona(c);
            }

            return RedirectToAction("Details/"+anuncio, "Postagem");
            
        }
        public ActionResult Bombar(int anuncio, int valor){
            IList<Bomba> b = bombaDAO.Lista();
            IList<BombaModel> BombaModel = new List<BombaModel>();
            Bomba bomba = new Bomba();
            bool teste = false;
            int total = 0;
            foreach (Bomba bb in b)
            {
               
                if (bb.UsuarioId == WebSecurity.CurrentUserId)
                {
                    bb.valor = valor; 
                    bombaDAO.Editar(bb);
                    teste = true;
                }
               
            }
            if (teste == false)
            {
                bomba.valor = valor;
                bomba.UsuarioId = WebSecurity.CurrentUserId;
                bomba.AnuncioId = anuncio;
                bombaDAO.Adiciona(bomba);
            }


            foreach (Bomba bb in b)
            {

                if (bb.AnuncioId == anuncio)
                {
                    total = bb.valor;

                }

            }
            Anuncio a = anuncioDAO.BuscarPorId(anuncio);
            a.total = total;
            anuncioDAO.Editar(a);
            return RedirectToAction("Details/" + anuncio, "Postagem");
        }
        /*
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                contexto.Dispose();
            }
            base.Dispose(disposing);
        }*/
    }
}
