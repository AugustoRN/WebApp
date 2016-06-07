using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Domain
{
    public class Usuario
    {
        public int Id { get; set; }

        public string Nome { get; set; }
        [Required]
        public string Senha { get; set; }        

        [Required, EmailAddress]
        public string Email { get; set; }

      

    }
}