using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApp.Domain;

namespace WebApp.Models
{
    public class ChatModel
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public int AnuncioId { get; set; }
        public Usuario Usuario { get; set; }
        public Anuncio Anuncio { get; set; }
        [Required]
        public string Texto { get; set; }
    }
}