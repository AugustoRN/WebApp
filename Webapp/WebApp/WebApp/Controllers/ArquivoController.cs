using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.ContextoBD;

namespace WebApp.Controllers
{
    public class ArquivoController : Controller
    {
        
        public ActionResult ExibirImagem(int id)
        {
            using(Contexto db = new Contexto())
            {
                var arquivoRetorn = db.Anuncios.Find(id);
                return File(arquivoRetorn.Imagem, arquivoRetorn.ImagemTipo);
            }
        }
        
    }
}