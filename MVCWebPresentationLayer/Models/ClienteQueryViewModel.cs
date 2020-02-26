using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCWebPresentationLayer.Models
{
    public class ClienteQueryViewModel
    {
        //Pra criar a tabela de pesquisa
        public int ID { get; set; }

        public string Nome { get; set; }
        
        public string CPF { get; set; }
        
        public string Email { get; set; }
        
        public DateTime DataNascimento { get; set; }
    }
}