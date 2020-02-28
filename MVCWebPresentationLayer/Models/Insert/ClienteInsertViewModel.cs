using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCWebPresentationLayer.Models
{
    //ViewModel só representa a tela.
    public class ClienteInsertViewModel
    {
        public string Nome { get; set; }

        public string CPF { get; set; }

        public DateTime DataNascimento { get; set; }

        public string Email { get; set; }

        public string Senha { get; set; }

        public string ConfirmarSenha { get; set; }
    }
}