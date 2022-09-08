using AutoMapper;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WEBPresentationLayer.Models.Funcionario;

namespace WEBPresentationLayer.Controllers
{
    public class LoginController : Controller
    {
        private readonly IMapper mapper;
        
        public LoginController(IMapper mapper)
        {
            this.mapper = mapper;
        }
        
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Logar(FuncionarioLoginViewModel funcionarioLogin)
        {
            //Funcionario funcionario = mapper.Map<Funcionario>(funcionarioLogin);
            HttpClient httpClient = new();
            string data = JsonConvert.SerializeObject(funcionarioLogin);
            StringContent stringContent = new(data);
            HttpResponseMessage message = await httpClient.PostAsync("localhost:5000/Funcionario/Logar", stringContent);
            return View(message);
        }
    }
}
