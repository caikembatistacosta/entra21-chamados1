using AutoMapper;
using BLL.Interfaces;
using Common;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WEBPresentationLayer.Models.Demanda;

namespace WEBPresentationLayer.Controllers
{
    //[Authorize]
    public class DemandaController : Controller
    {
        private readonly HttpClient _httpClient;
        public DemandaController(HttpClient httpClient)
        {
            httpClient.BaseAddress = new Uri("https://localhost:7202/");
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Index()
        {

            HttpResponseMessage response = await _httpClient.GetAsync("Demanda/All-Demanda");
            response.EnsureSuccessStatusCode();
            string json = await response.Content.ReadAsStringAsync();
            Object? chamado = JsonConvert.DeserializeObject(json);
            if (chamado == null)
            {
                return NotFound();
            }
            return View(chamado);
        }
        [HttpGet("Insert-Demands")]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(DemandaInsertViewModel viewModel)
        {
            HttpResponseMessage message = await _httpClient.PostAsJsonAsync<DemandaInsertViewModel>("Demanda/Insert-Demands", viewModel);
            Task<string> content = message.Content.ReadAsStringAsync();

            if (content.Result.Contains("400"))
                return NotFound();

            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            HttpResponseMessage response = await _httpClient.GetAsync("Demanda/Edit-Demands");
            response.EnsureSuccessStatusCode();
            string json = await response.Content.ReadAsStringAsync();
            Object? chamado = JsonConvert.DeserializeObject(json);
            if (chamado == null)
                return NotFound();

            return View(chamado);

        }
        [HttpPost]
        public async Task<IActionResult> Edit(DemandaUpdateViewModel viewModel)
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync<DemandaUpdateViewModel>("Demanda/Edit-Demands", viewModel);
            Task<string> content = httpResponseMessage.Content.ReadAsStringAsync();
            if (content.Result.Contains("400"))
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }
        //public async Task<IActionResult> Details(int id)
        //{
           
        //}

        //[HttpPost]
        //[Route("ChangeStatusInProgress")]
        //public async Task<IActionResult> ChangeStatusInProgress(DemandaUpdateViewModel viewModel)
        //{
        //    Demanda Demanda = _mapper.Map<Demanda>(viewModel);
        //    Response response = await _Demandasvc.ChangeStatusInProgress(Demanda);
        //    if (response.HasSuccess)
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewBag.Errors = response.Message;
        //    return View(Demanda);
        //}


    }
}
