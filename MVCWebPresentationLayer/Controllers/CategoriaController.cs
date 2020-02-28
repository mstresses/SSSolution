﻿using AutoMapper;
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
    public class CategoriaController : Controller
    {
        [HttpGet]
        public ActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Cadastrar(CategoriaInsertViewModel categoriaViewModel)
        {
            CategoriaService svc = new CategoriaService();

            var configuration = new MapperConfiguration(cfg => { cfg.CreateMap<CategoriaInsertViewModel, CategoriaDTO>(); });

            IMapper mapper = configuration.CreateMapper();

            CategoriaDTO dto = mapper.Map<CategoriaDTO>(categoriaViewModel);

            try
            {
                await svc.Insert(dto);
                ViewBag.MensagemSucesso = ("Cadastrado com sucesso!");
                return RedirectToAction("Index", "Categoria");
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
                CategoriaService svc = new CategoriaService();

                List<CategoriaDTO> categorias = await svc.GetCategories(1, 10);

                var configuration = new MapperConfiguration(cfg => { cfg.CreateMap<CategoriaDTO, CategoriaQueryViewModel>(); });

                IMapper mapper = configuration.CreateMapper();

                List<CategoriaQueryViewModel> dados = mapper.Map<List<CategoriaQueryViewModel>>(categorias);

                return View(dados);
            }
            catch (Exception)
            {
                return View();
            }
        }
    }
}