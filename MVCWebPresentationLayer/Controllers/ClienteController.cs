using MVCWebPresentationLayer.Mock;
using MVCWebPresentationLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCWebPresentationLayer.Controllers
{
    public class ClienteController : Controller
    {
        [HttpGet]
        public ActionResult Cadastrar()
        {
            return View();
        }

        //Precisa condecorar ele, e significa que vai receber dados.
        [HttpPost]
        public ActionResult Cadastrar(CadastroClienteViewModel clienteViewModel)
        {
            ClienteMockBLL mockBLL = new ClienteMockBLL();
            try
            {
                //Esta linha pode lançar uma exception, logo ela deve estar dentro e um bloco try.
                mockBLL.Cadastrar(clienteViewModel);

                //Se chegou aqui, deu boa, não deu exception! Redirecione o usuáro para a página inicial.
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                //Se chegou aqui, o método Cadastrar do MockBLL deu erro :(

                ViewBag.MensagemErro = ex.Message;

                //Retorna a mesma tela que o usuário estava
                return View();
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