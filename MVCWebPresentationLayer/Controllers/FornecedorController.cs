using AutoMapper;
using BLL.Impl;
using Common;
using DTO;
using MVCWebPresentationLayer.Models;
using Remote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MVCWebPresentationLayer.Controllers
{
    public class FornecedorController : Controller
    {
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(FornecedorInsertViewModel fornecedorViewModel)
        {
            FornecedorService svc = new FornecedorService();

            var configuration = new MapperConfiguration(cfg => { cfg.CreateMap<FornecedorInsertViewModel, FornecedorDTO>(); });

            IMapper mapper = configuration.CreateMapper();

            FornecedorDTO dto = mapper.Map<FornecedorDTO>(fornecedorViewModel);

            try
            {
                await svc.Create(dto);
                ViewBag.MensagemSucesso = ("Cadastrado com sucesso!");
                return RedirectToAction("Index", "Fornecedor");
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
        public async Task<ActionResult> Index()
        {
            try
            {
                FornecedorService svc = new FornecedorService();
                List<FornecedorDTO> fornecedores = await svc.GetSuppliers(1, 10);

                var configuration = new MapperConfiguration(cfg => { cfg.CreateMap<FornecedorDTO, FornecedorQueryViewModel>(); });

                IMapper mapper = configuration.CreateMapper();

                //Transforma o ClienteDTO em um ClienteQueryViewModel e trás os intens pra tela.
                //Este objeto "dados" é uma lista de objetos ViewModel.
                List<FornecedorQueryViewModel> dados = mapper.Map<List<FornecedorQueryViewModel>>(fornecedores);

                //Os dados no index(html) vira Model(objeto).
                return View(dados);
            }
            catch (Exception)
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult PesquisaCEP(string cep)
        {
            cep = cep.Replace("-", "");
            CEPRemoteService cepSvc = new CEPRemoteService(cep);

            //OBJETO ANONIMO
            var obj = new { TipoLogradouro = cepSvc.TipoLogradouro, Logradouro = cepSvc.Logradouro, Bairro = cepSvc.Bairro, Cidade = cepSvc.Cidade, UF = cepSvc.UF };
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
    }
}