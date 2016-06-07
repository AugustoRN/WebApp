using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace WebApp.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Autenticar(String login, String senha)
        {
            if (WebSecurity.Login(login, senha))
            {
                return RedirectToAction("Anunciar", "Postagem");
            }
            else
            {
                
                TempData["mensagem"] = string.Format("{0}: Login ou senha incorretos", login);
                return View("Index");
            }
        }
        public ActionResult Logout()
        {
            WebSecurity.Logout();
            return RedirectToAction("Index");
        }
    }
}