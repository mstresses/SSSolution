using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCWebPresentationLayer.Models
{
    public class CategoriaInsertViewModel
    {
        //Data anotations
        [Required(ErrorMessage = "O nome deve ser informado.")]
        [StringLength(maximumLength: 40, MinimumLength = 5, ErrorMessage = "O nome da categoria deve conter entre 5 e 40 caracteres.")]
        public string Categoria { get; set; }
    }
}