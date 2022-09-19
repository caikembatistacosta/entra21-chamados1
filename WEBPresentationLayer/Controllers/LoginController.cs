using AutoMapper;
using Common;
using Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Reflection.Metadata;
using System.Security.Claims;
using WEBPresentationLayer.Models.Funcionario;

namespace WEBPresentationLayer.Controllers
{
    public class LoginController : Controller
    {
        private readonly HttpClient _httpClient;
        public LoginController(HttpClient httpClient)
        {
            httpClient.BaseAddress = new Uri("https://localhost:7202/");
            _httpClient = httpClient;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Logar(FuncionarioLoginViewModel funcionarioLogin)
        {
            try
            {
                HttpResponseMessage message = await _httpClient.PostAsJsonAsync<FuncionarioLoginViewModel>("Login/Logar", funcionarioLogin);
                string content = await message.Content.ReadAsStringAsync();
                FuncionarioLoginViewModel f = JsonConvert.DeserializeObject<FuncionarioLoginViewModel>(content);
                if (f == null)
                {
                    return NotFound();
                }
                if (!message.IsSuccessStatusCode)
                {
                    return NotFound();
                }
                List<Claim> userClaims = new()
                {
                    new Claim(ClaimTypes.NameIdentifier, f.Email),
                    new Claim(ClaimTypes.Email, f.Email),
                    new Claim(ClaimTypes.Sid, f.Token)
                };
                ClaimsIdentity minhaIdentity = new(userClaims, "Email");
                ClaimsPrincipal userPrincipal = new(new[] { minhaIdentity });
                //userPrincipal.IsInRole(f.Token);
                await HttpContext.SignInAsync(userPrincipal, new AuthenticationProperties
                {
                    IsPersistent = true,
                    AllowRefresh = true,
                    ExpiresUtc = DateTime.UtcNow.AddDays(30)
                });
                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {
                return NotFound();
            }
           
        }
        public async Task<IActionResult> Logoff()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public async Task<IActionResult> Refresh(string token,string refreshToken)
        {
            HttpResponseMessage message = await _httpClient.GetAsync($"Login/Refresh?token={token}&refreshToken={refreshToken}");
            string content = await message.Content.ReadAsStringAsync();
            if (!message.IsSuccessStatusCode)
            {
                return NotFound();
            }
            dynamic json = JsonConvert.DeserializeObject(content);
            if(json == null)
            {
                return NotFound();
            }
            return RedirectToAction("Index", "Home");
        }

    }
}
