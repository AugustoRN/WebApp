﻿using System;
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
        private AnuncioRepositorio anuncioDAO;
        private UsuarioRepositorio usuarioDAO;
       

        public PostagemController(AnuncioRepositorio anuncioDAO, UsuarioRepositorio usuarioDAO)
        {
            this.anuncioDAO = anuncioDAO;
            this.usuarioDAO = usuarioDAO;

        }

        public ActionResult Anunciar(int? pagina)
        {
            int tamanhoPagina = 2;
            int numeroPagina = pagina ?? 1;

            return View(anuncioDAO.GetAll().ToPagedList(numeroPagina, tamanhoPagina));
        }
        
      

        // GET: Postagem/Details/5
        public ActionResult Details(int? id)
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
                anuncioDAO.Editar(anuncio);
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
            Anuncio anuncio = anuncioDAO.BuscarPorId(id);
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
            Anuncio anuncio = anuncioDAO.BuscarPorId(id);
            anuncioDAO.Remover(anuncio);
            TempData["mensagem"] = string.Format("{0}: foi Deletado com sucesso", anuncio.Titulo);
            return RedirectToAction("Anunciar");
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
