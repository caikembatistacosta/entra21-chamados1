using AutoMapper;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WEBPresentationLayer.Models.Funcionario;

namespace WEBPresentationLayer.Controllers
{
    public class LoginController : Controller
    {
        
        public LoginController()
        {

        }
        
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Logar(FuncionarioLoginViewModel funcionarioLogin)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(@"https://localhost:7234/");
            string data = JsonConvert.SerializeObject(funcionarioLogin);
            StringContent stringContent = new(data);
            HttpResponseMessage message = await httpClient.PostAsJsonAsync<FuncionarioLoginViewModel>("Login/Logar", funcionarioLogin);
            var content = message.Content.ReadAsStringAsync();
            if (!content.IsCompletedSuccessfully)
            {
                return NotFound();
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
