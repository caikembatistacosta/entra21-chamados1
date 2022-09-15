﻿using AutoMapper;
using BLL.Impl;
using BLL.Interfaces;
using Common;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WEBPresentationLayer.Models.Cliente;

namespace WEBPresentationLayer.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class ClienteController : Controller
    {
        private readonly HttpClient _httpClient;
        public ClienteController(HttpClient httpClient)
        {
            httpClient.BaseAddress = new Uri("https://localhost:7202/");
            _httpClient = httpClient;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                HttpResponseMessage message = await _httpClient.GetAsync("Cliente/All-Costumers");
                //message.EnsureSuccessStatusCode();
                string json = await message.Content.ReadAsStringAsync();
                Cliente? cliente = JsonConvert.DeserializeObject<Cliente>(json);
                if (cliente == null)
                    return NotFound();
                return View(cliente);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
          
        }
        [HttpGet("Insert-Costumer")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ClienteInsertViewModel viewModel)
        {

            HttpResponseMessage message = await _httpClient.PostAsJsonAsync<ClienteInsertViewModel>("Cliente/Insert-Costumer", viewModel);
            Task<string> content = message.Content.ReadAsStringAsync();

            if (content.Result.Contains("400"))
                return NotFound();

            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            HttpResponseMessage response = await _httpClient.GetAsync("Cliente/Edit-Costumer/");
            response.EnsureSuccessStatusCode();
            string json = await response.Content.ReadAsStringAsync();
            Object? cliente = JsonConvert.DeserializeObject(json);
            if (cliente == null)
                return NotFound();

            return View(cliente);

        }
        [HttpPost]
        public async Task<IActionResult> Edit(ClienteUpdateViewModel viewModel)
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync<ClienteUpdateViewModel>("Cliente/Update-Costumer", viewModel);
            Task<string> content = httpResponseMessage.Content.ReadAsStringAsync();

            if (content.Result.Contains("400"))
                return NotFound();

            return RedirectToAction(nameof(Index));
        }

        //public IActionResult Delete()
        //{
        //    return View();
        //}

    }
}
