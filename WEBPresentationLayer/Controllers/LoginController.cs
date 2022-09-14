using AutoMapper;
using BLL.Interfaces;
using Common;
using Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
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

            HttpResponseMessage message = await _httpClient.PostAsJsonAsync<FuncionarioLoginViewModel>("Login/Logar", funcionarioLogin);
            Task<string> content = message.Content.ReadAsStringAsync();
            if (content.Result.Contains("400"))
            {
                return NotFound();
            }
            var userClaims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier, funcionarioLogin.Email),
                    new Claim(ClaimTypes.Email, funcionarioLogin.Email)
                };
            var minhaIdentity = new ClaimsIdentity(userClaims, "Usuario");
            var userPrincipal = new ClaimsPrincipal(new[] { minhaIdentity });
            await HttpContext.SignInAsync(userPrincipal, new AuthenticationProperties
            {
                IsPersistent = true,
                AllowRefresh = true,
                ExpiresUtc = DateTime.UtcNow.AddDays(30)
            });
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> Logoff()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

    }
}
