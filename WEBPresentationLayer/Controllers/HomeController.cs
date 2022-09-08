using BLL.Impl;
using BLL.Interfaces;
using Common;
using Entities;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WEBPresentationLayer.Models;

namespace WEBPresentationLayer.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IChamadoService _chamadoService;

        public HomeController(ILogger<HomeController> logger, IChamadoService chamadoService)
        {
            _logger = logger;
            this._chamadoService = chamadoService;
        }

        public async Task<IActionResult> Index()
        {
            //SUBSTITUIR DEPOIS PELA CHAMADA DA WEB API DO DAVI
            DataResponse<Chamado> chamadosResponse = await _chamadoService.GetAll();
            return View(chamadosResponse.Data);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}