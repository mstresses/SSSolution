using MVCWebPresentationLayer.Mock;
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
        public ActionResult Testar()
        {
            return View();
        }

        //HttpPost, significa que este método irá atender uma requisição feita no back-end via click do botão em um form com httppost da View Testar.
        [HttpPost]
        public ActionResult Testar(ProdutoViewModel viewModel) //IGUAL AO BUTTON_CLICK
        {
            //O objeto viewModel virá preenchido de acordo com os names dos inputs do form que teve seu botão clicado.
            ProdutoMockBLL mockBLL = new ProdutoMockBLL();
            try
            {
                //Esta linha pode lançar uma exception, logo ela deve estar dentro e um bloco try.
                mockBLL.Cadastrar(viewModel);

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
        public ActionResult Cadastrar()
        {
            return View();
        }

        //Precisa condecorar ele, e significa que vai receber dados.
        [HttpPost]
        public ActionResult Cadastrar(ProdutoViewModel viewModel)
        {
            return View();
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}