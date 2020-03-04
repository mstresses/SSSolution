using AutoMapper;
using BLL.Impl;
using Common;
using DTO;
using MVCWebPresentationLayer.Models;
using MVCWebPresentationLayer.Models.Insert;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MVCWebPresentationLayer.Controllers
{
    public class UsuarioController : Controller
    {
        UsuarioService userService = new UsuarioService();

        // GET: Usuario
        [HttpGet]
        public async Task<ActionResult> Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Cadastrar(UsuarioInsertViewModel usuarioViewModel)
        {
            UsuarioService svc = new UsuarioService();
            var configuration = new MapperConfiguration(cfg => { cfg.CreateMap<UsuarioInsertViewModel, UsuarioDTO>(); });

            IMapper mapper = configuration.CreateMapper();

            UsuarioDTO dto = mapper.Map<UsuarioDTO>(usuarioViewModel);
            try
            {
                await svc.Insert(dto);
                ViewBag.MensagemSucesso = ("Cadastrado com sucesso!");
                return RedirectToAction("Index", "Usuario");
            }
            catch (NecoException ex)
            {
                ViewBag.ValidationErrors = ex.Errors;
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
            }
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(string email, string senha)
        {
            try
            {
                var usuario = await userService.Authenticate(email, senha);
                HttpCookie cookie = new HttpCookie("USERIDENTITY", usuario.ID.ToString());
                //Se chegou aqui, sucesso.
            }
            catch (Exception ex)
            {
                ViewBag.Erros = ex.Message;
            }
            return View();
        }
    }
}