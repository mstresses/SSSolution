using AutoMapper;
using BLL.Impl;
using Common;
using DTO;
using MVCWebPresentationLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task<ActionResult> Inserir(ClienteInsertViewModel clienteViewModel) //Método dentro do controller define para onde vai o site.
        {
            ClienteService svc = new ClienteService();

            //Transforma o DTO em ViewModel (AUTOMAPPER) para jogar no service
            var configuration = new MapperConfiguration(cfg => {cfg.CreateMap<ClienteInsertViewModel, ClienteDTO>();});

            IMapper mapper = configuration.CreateMapper();

            //Transforma o ClienteInsertViewModel em um ClienteDTO.
            ClienteDTO dto = mapper.Map<ClienteDTO>(clienteViewModel);

            try
            {
                await svc.Insert(dto);
                //Se funcionou, redireciona pra página inicial.
                ViewBag.MensagemSucesso = ("Cadastrado com sucesso!");
                return RedirectToAction("Index", "Cliente");
            }
            catch (NecoException ex)
            {
                //Se caiu aqui, o ClienteService lançou a excessão por validação de campos.
                ViewBag.ValidationErrors = ex.Errors;
            }
            catch (Exception ex)
            {
                //Se caiu aqui, o ClienteService lançou a excessãogenérica, provavelmente falha no acesso ao banco.
                ViewBag.ErrorMessage = ex.Message;
            }
            return View();
        }

        //meusite.com/Cliente
        //meusite.com/Cliente/Index
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            try
            {
                ClienteService svc = new ClienteService();
                List<ClienteDTO> clientes = await svc.GetCustomers(1, 10);

                var configuration = new MapperConfiguration(cfg => {cfg.CreateMap<ClienteDTO, ClienteQueryViewModel>();});

                IMapper mapper = configuration.CreateMapper();

                //Transforma o ClienteDTO em um ClienteQueryViewModel e trás os intens pra tela.
                //Este objeto "dados" é uma lista de objetos ViewModel.
                List<ClienteQueryViewModel> dados = mapper.Map<List<ClienteQueryViewModel>>(clientes);

                //Os dados no index(html) vira Model(objeto).
                return View(dados);
            }
            catch (Exception)
            {
                return View();
            }
        }
    }
}