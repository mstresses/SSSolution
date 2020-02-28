using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCWebPresentationLayer.Models
{
    public class FornecedorInsertViewModel
    {
        public int ID { get; set; }

        public string Fornecedor { get; set; }
        
        public string Email { get; set; }
        
        public string CNPJ { get; set; }
    }
}