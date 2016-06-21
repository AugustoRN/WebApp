using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Domain
{
    public class Bomba
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
         public int AnuncioId { get; set; }
        
        
        [Required]
        public int valor { get; set; }
    }
}