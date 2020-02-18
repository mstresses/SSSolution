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
        //Métodos Http mais populares são GET e POST. Por padrão, todo hiperlink ou url digitada manualmente, efetuará chamada ao servidor utilizando GET.
        //O POST é utilizado quando queremos ENVIAR dados ao servidor, então é mais comum termos ele em um form com vários componentes inputs.
        [HttpGet]
        public ActionResult Inserir()
        {
            return View();
        }

        //Precisa condecorar ele, e significa que vai receber dados. SIMILAR AO BUTTON_CLICK
        [HttpPost]
        public ActionResult Inserir(ClienteInsertViewModel clienteViewModel) //Método dentro do controller define para onde vai o site.
        {
            ClienteMockBLL mockBLL = new ClienteMockBLL();
            try
            {
                //Esta linha pode lançar uma exception, logo ela deve estar dentro e um bloco try.
                mockBLL.Cadastrar(clienteViewModel);

                ViewBag.Sucesso = "Cliente cadastrado com sucesso.";
                return View();
            }
            catch (Exception ex)
            {
                //Se chegou aqui, o método Cadastrar do MockBLL deu erro :(
                ViewBag.MensagemErro = ex.Message;

                //Retorna a mesma tela que o usuário estava
                return View();
            }
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}