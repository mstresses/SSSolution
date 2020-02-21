using AutoMapper;
using BLL.Impl;
using Common;
using DTO;
using MVCWebPresentationLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCWebPresentationLayer.Controllers
{
    public class ProdutoController : Controller
    {
        [HttpGet]
        public ActionResult Cadastrar()
        {
            return View();
        }

        //HttpPost, significa que este método irá atender uma requisição feita no back-end via click do botão em um form com httppost da View Testar.
        [HttpPost]
        public ActionResult Cadastrar(ProdutoInsertViewModel viewModel) //IGUAL AO BUTTON_CLICK
        {
            ProdutoService svc = new ProdutoService();

            //Transforma o DTO em ViewModel (AUTOMAPPER)
            var configuration = new MapperConfiguration(cfg => 
            {
                cfg.CreateMap<ProdutoInsertViewModel, ProdutoDTO>();
            });

            IMapper mapper = configuration.CreateMapper();

            //Transforma o ProdutoInsertViewModel em um ProdutoDTO.
            ProdutoDTO dto = mapper.Map<ProdutoDTO>(viewModel);

            try
            {
                svc.Insert(dto);
                //Se funcionou, redireciona pra página inicial.
                //return RedirectToAction("Home", "Index");
                ViewBag.MensagemSucesso = ("Cadastrado com sucesso!");
                return View();
            }
            catch (NecoException ex)
            {
                //Se caiu aqui, o ProdutoService lançou a excessão por validação de campos.
                ViewBag.ValidationErrors = ex.Errors;
            }
            catch (Exception ex)
            {
                //Se caiu aqui, o ProdutoService lançou uma excessão genérica, provavelmente falha no acesso ao banco.
                ViewBag.ErrorMessage = ex.Message;
            }
            return View();
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}