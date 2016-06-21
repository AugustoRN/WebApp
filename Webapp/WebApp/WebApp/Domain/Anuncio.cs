using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Domain
{
    public class Anuncio
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao {get; set;}
        public byte[] Imagem { get; set; }
        public string ImagemTipo { get; set; }       

        public int UsuarioId { get; set; }

        public virtual Usuario Usuario { get; set; }

        public int total { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:c}")]
        public decimal Valor { get; set; }

    }
}