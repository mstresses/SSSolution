using AutoMapper;
using BLL.Impl;
using Common;
using DTO;
using MVCWebPresentationLayer.Models.Insert;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MVCWebPresentationLayer.Controllers
{
    public class UsuarioController : BaseController
    {
        UsuarioService userService = new UsuarioService();

        // GET: Usuario
        public ActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Cadastrar(UsuarioInsertViewModel viewModel)
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UsuarioInsertViewModel, UsuarioDTO>();
            });

            IMapper mapper = configuration.CreateMapper();
            UsuarioDTO dto = mapper.Map<UsuarioDTO>(viewModel);
            try
            {
                await userService.Insert(dto);
            }
            //catch(NecoException ex)
            //{
            // Tratamento de erros do service, ver o exemplo do cadastro de produto/cliente
            //}
            catch (Exception ex)
            {
                ViewBag.Erros = ex.Message;
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
                UsuarioDTO usuario = await userService.Authenticate(email, senha);
                HttpCookie cookie = new HttpCookie("USERIDENTITY", usuario.ID.ToString()); //GRAVAR USERIDENTITY
                cookie.Expires = DateTime.MaxValue;
                Response.Cookies.Add(cookie);
                return RedirectToAction("Index", "Cliente");
            }
            catch (Exception ex)
            {
                ViewBag.Erros = ex.Message;
            }
            return View();
        }
    }
}