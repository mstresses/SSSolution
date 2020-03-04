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
    public class ProdutoController : Controller
    {
        FornecedorService fsvc = new FornecedorService();
        CategoriaService csvc = new CategoriaService();

        public async Task<ActionResult> Cadastrar()
        {
            List<FornecedorDTO> fornecedores = await fsvc.GetSuppliers();
            List<CategoriaDTO> categorias = await csvc.GetCategories();

            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<FornecedorDTO, FornecedorQueryViewModel>();
                cfg.CreateMap<CategoriaDTO, CategoriaQueryViewModel>();
            });

            IMapper mapper = configuration.CreateMapper();
            // new SERService().GetSERByID(4);
            //Transforma o ClienteInsertViewModel em um ClienteDTO

            //Este objeto "dados" é uma lista de objetos ViewModel
            List<FornecedorQueryViewModel> dadosFornecedores = mapper.Map<List<FornecedorQueryViewModel>>(fornecedores);
            List<CategoriaQueryViewModel> dadosCategorias = mapper.Map<List<CategoriaQueryViewModel>>(categorias);

            ViewBag.Fornecedores = dadosFornecedores;
            ViewBag.Categorias = dadosCategorias;

            return View();
        }
        //HttpPost, significa que este método irá atender uma requisição feita no back-end via click do botão em um form com httppost da View Testar.
        [HttpPost]
        public async Task<ActionResult> Cadastrar(ProdutoInsertViewModel viewModel) //IGUAL AO BUTTON_CLICK
        {
            ProdutoService svc = new ProdutoService();

            //Transforma o DTO em ViewModel (AUTOMAPPER)
            var configuration = new MapperConfiguration(cfg => { cfg.CreateMap<ProdutoInsertViewModel, ProdutoDTO>(); });

            IMapper mapper = configuration.CreateMapper();
            //Transforma o ProdutoInsertViewModel em um ProdutoDTO.
            ProdutoDTO dto = mapper.Map<ProdutoDTO>(viewModel);

            try
            {
                await svc.Insert(dto);
                //Se funcionou, redireciona pra página inicial.
                ViewBag.MensagemSucesso = ("Cadastrado com sucesso!");
                return RedirectToAction("Index", "Produto");
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
        public async Task<ActionResult> Index()
        {
            try
            {
                ProdutoService svc = new ProdutoService();
                List<ProdutoDTO> produtos = await svc.GetProducts(1, 10);

                var configuration = new MapperConfiguration(cfg => { cfg.CreateMap<ProdutoDTO, ProdutoQueryViewModel>(); });

                IMapper mapper = configuration.CreateMapper();

                //Transforma o ClienteDTO em um ClienteQueryViewModel e trás os intens pra tela.
                //Este objeto "dados" é uma lista de objetos ViewModel.
                List<ProdutoQueryViewModel> dados = mapper.Map<List<ProdutoQueryViewModel>>(produtos);

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