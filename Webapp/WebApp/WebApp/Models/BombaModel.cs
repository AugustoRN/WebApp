using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApp.Domain;

namespace WebApp.Models
{
    public class BombaModel
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public int AnuncioId { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual Anuncio Anuncio { get; set; }


        [Required]
        public int valor { get; set; }
    }
}