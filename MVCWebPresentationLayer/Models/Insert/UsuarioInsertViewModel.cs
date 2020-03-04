using DTO.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCWebPresentationLayer.Models.Insert
{
    public class UsuarioInsertViewModel
    {
        [DisplayName("Nome")]
        [Required(ErrorMessage = "O nome deve ser informado")]
        [StringLength(maximumLength: 100, MinimumLength = 5, ErrorMessage = "O nome deve conter entre 5 e 100 caracteres")]
        public string Nome { get; set; }

        [DisplayName("Email")]
        [Required(ErrorMessage = "O email deve ser informado")]
        [StringLength(maximumLength: 100, MinimumLength = 5, ErrorMessage = "O email deve conter entre 5 e 100 caracteres")]
        public string Email { get; set; }

        [DisplayName("Senha")]
        [Required(ErrorMessage = "A senha deve ser informado")]
        [StringLength(maximumLength: 20, MinimumLength = 5, ErrorMessage = "O email deve conter entre 5 e 20 caracteres")]
        public string Senha { get; set; }

        public PermissaoUsuario PermissaoUsuario { get; set; }
    }
}