using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCWebPresentationLayer.Models
{
    public class CategoriaInsertViewModel
    {
        public int ID { get; set; }
        public string Categoria { get; set; }
    }
}