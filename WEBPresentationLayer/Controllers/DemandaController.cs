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
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync("All-Demands");
                response.EnsureSuccessStatusCode();
                string json = await response.Content.ReadAsStringAsync();
                List<DemandaSelectViewModel>? chamado = JsonConvert.DeserializeObject<List<DemandaSelectViewModel>>(json);
                if (chamado == null)
                {
                    return NotFound();
                }
                return View(chamado);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
           
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(DemandaInsertViewModel viewModel)
        {
            HttpResponseMessage message = await _httpClient.PostAsJsonAsync<DemandaInsertViewModel>("Insert-Demands", viewModel);
            string content = await message.Content.ReadAsStringAsync();

            if (content.Contains("400"))
                return NotFound();

            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"Edit-Demands?id={id}");
                response.EnsureSuccessStatusCode();
                string json = await response.Content.ReadAsStringAsync();
                DemandaUpdateViewModel? chamado = JsonConvert.DeserializeObject<DemandaUpdateViewModel>(json);
                if (chamado == null)
                    return NotFound();

                return View(chamado);
            }
            catch (Exception)
            {
                return NotFound();
            }
            

        }
        [HttpPost]
        public async Task<IActionResult> Edit(DemandaUpdateViewModel viewModel)
        {
            try
            {
                HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync<DemandaUpdateViewModel>("Edit-Demands", viewModel);
                string content = await httpResponseMessage.Content.ReadAsStringAsync();
                if (content.Contains("400"))
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return NotFound();
            }
           
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
