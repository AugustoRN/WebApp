using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Domain;
using WebApp.Repositorio;

namespace WebApp.Controllers
{
    public class UsuarioController : Controller
    {
        private UsuarioRepositorio usuarioDAO;

        public UsuarioController(UsuarioRepositorio usuarioDAO)
        {
            this.usuarioDAO = usuarioDAO;
        }

        public ActionResult Form()
        {
            return View();
        }

        public ActionResult Adiciona(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                usuarioDAO.Adiciona(usuario);
                TempData["mensagem"] = string.Format("O usuario  {0} foi incluido com sucesso", usuario.Email);
                return RedirectToAction("Anunciar","Postagem");
                
            }
            else
            {
                return View("Form", usuario);
            }
        
        }

    }
}