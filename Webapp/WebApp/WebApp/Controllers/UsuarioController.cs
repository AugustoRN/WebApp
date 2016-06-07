using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebApp.Domain;
using WebApp.Models;
using WebApp.Repositorio;
using WebMatrix.WebData;

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

        public ActionResult Adiciona(UsuarioModel usuarioModel)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    WebSecurity.CreateUserAndAccount(usuarioModel.Email, usuarioModel.Senha,
                    new { Nome = usuarioModel.Nome });

                }
                catch (MembershipCreateUserException e)
                {
                    TempData["mensagem"] = string.Format("O usuario  {0} é invalido", usuarioModel.Email);
                    return View("Form", usuarioModel);
                }
               
                TempData["mensagem"] = string.Format("O usuario  {0} foi incluido com sucesso", usuarioModel.Email);
                return RedirectToAction("Anunciar","Postagem");
                
            }
            else
            {
                return View("Form", usuarioModel);
            }
        
        }

    }
}