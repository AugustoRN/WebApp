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
namespace WebApp.Controllers
{
    public class PostagemController : Controller
    {
        private Contexto db = new Contexto();
        private IRepositorio<Anuncio> _repositorioAnuncio;

         public PostagemController()
        {
            _repositorioAnuncio = new AnuncioRepositorio(new Contexto());

        }
        public ActionResult Anunciar(int? pagina)
        {
            int tamanhoPagina = 5;
            int numeroPagina = pagina ?? 1;

            return View(_repositorioAnuncio.GetAll().ToPagedList(numeroPagina,tamanhoPagina));
        }


        
      

        // GET: Postagem/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Anuncio anuncio = db.Anuncios.Find(id);
            if (anuncio == null)
            {
                return HttpNotFound();
            }
            return View(anuncio);
        }

        // GET: Postagem/Create
        public ActionResult Create()
        {
            return View();
        }

    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Titulo,Descricao,Imagem,ImagemTipo,Contador")] Anuncio anuncio,HttpPostedFileBase upload)
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
                db.Anuncios.Add(anuncio);
                db.SaveChanges();
                TempData["mensagem"] = string.Format("{0}: foi incluido com sucesso", anuncio.Titulo);
                return RedirectToAction("Anunciar");
            }

            return View(anuncio);
        }

       //get
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Anuncio anuncio = db.Anuncios.Find(id);
            if (anuncio == null)
            {
                return HttpNotFound();
            }
            return View(anuncio);
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Titulo,Descricao,Imagem,ImagemTipo,Contador")] Anuncio anuncio,HttpPostedFileBase upload)
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
                db.Entry(anuncio).State = EntityState.Modified;
                db.SaveChanges();
                TempData["mensagem"] = string.Format("{0}: foi Editado com sucesso", anuncio.Titulo);
                return RedirectToAction("Anunciar");
            }

            return View(anuncio);
        }

        // GET: Postagem/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Anuncio anuncio = db.Anuncios.Find(id);
            if (anuncio == null)
            {
                return HttpNotFound();
            }
            return View(anuncio);
        }

        // POST: Postagem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Anuncio anuncio = db.Anuncios.Find(id);
            db.Anuncios.Remove(anuncio);
            db.SaveChanges();
            TempData["mensagem"] = string.Format("{0}: foi Deletado com sucesso", anuncio.Titulo);
            return RedirectToAction("Anunciar");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
