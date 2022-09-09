using AutoMapper;
using BLL.Interfaces;
using Common;
using Entities;
using Microsoft.AspNetCore.Mvc;
using WEBApi.ApiConfig;
using WEBApi.Models.Funcionario;

namespace WEBApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : Controller
    {
        private readonly IFuncionarioService funcionarioService;
        private readonly IMapper mapper;
        public LoginController(IFuncionarioService funcionarioService, IMapper mapper)
        {
            this.funcionarioService = funcionarioService;
            this.mapper = mapper;
        }
        [HttpGet("Login")]
        public IActionResult Login()
        {
            return Ok();
        }

        [HttpPost("Logar")]
        public async Task<IActionResult> Logar(FuncionarioLoginViewModel funcionarioLogin)
        {
            Funcionario funcionario = mapper.Map<Funcionario>(funcionarioLogin);
            SingleResponse<Funcionario> singleResponse = await funcionarioService.GetLogin(funcionario);
            if (!singleResponse.HasSuccess)
            {
                return BadRequest();
            }
            var token = TokenService.GenerateToken(singleResponse.Item);
            funcionario.Senha = "";

            return Ok(token);

        }
    }
}
