using AutoMapper;
using BLL.Interfaces;
using Common;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Json;
using WEBPresentationLayer.Models.Chamado;

namespace WEBPresentationLayer.Controllers
{
    [Authorize]
    public class ChamadoController : Controller
    {
        private readonly HttpClient _httpClient;
        public ChamadoController(HttpClient httpClient)
        {
            httpClient.BaseAddress = new Uri("https://localhost:7234/");
            _httpClient = httpClient;
        }
       
        public async Task<IActionResult> Index()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("Chamado/All-Chamados");
            response.EnsureSuccessStatusCode();
            string json = await response.Content.ReadAsStringAsync();
            Object? chamado = JsonConvert.DeserializeObject(json);
            if(chamado == null)
            {
                return NotFound();
            }
            return View(chamado);//Desfazer o Json para virar um IEnumerable de Chamados

        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("Chamado/InsertChamado");
            response.EnsureSuccessStatusCode();
            
            string json = await response.Content.ReadAsStringAsync();
            Object? chamado = JsonConvert.DeserializeObject(json);
            
            if (chamado == null)
                return NotFound();
            
            return View(chamado); //Desfazer o Json para virar um ViewModel de Chamados
        }
        [HttpPost]
        public async Task<IActionResult> Create(ChamadoInsertViewModel viewModel)
        {
            HttpResponseMessage message = await _httpClient.PostAsJsonAsync<ChamadoInsertViewModel>("Chamado/InsertChamado", viewModel);
            Task<string> content = message.Content.ReadAsStringAsync();
           
            if (content.Result.Contains("400"))
                return NotFound();

            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            HttpResponseMessage response = await _httpClient.GetAsync("Chamado/Edit/");
            response.EnsureSuccessStatusCode();
            string json = await response.Content.ReadAsStringAsync();
            Object? chamado = JsonConvert.DeserializeObject(json);
            if(chamado == null) 
                return NotFound(); 
            
            return View(chamado);//Desfazer o Json para virar um ViewModel de Chamados
        }
        [HttpPost]
        public async Task<IActionResult> Edit(ChamadoUpdateViewModel viewModel)
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync<ChamadoUpdateViewModel>("Chamado/UpdateChamado", viewModel);
            Task<string> content = httpResponseMessage.Content.ReadAsStringAsync();
            if (content.Result.Contains("400"))
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));

        }
        public async Task<IActionResult> Details(int id)
        {
            return View();
        }


    }
}
