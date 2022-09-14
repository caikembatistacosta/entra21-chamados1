using AutoMapper;
using BLL.Interfaces;
using Common;
using Entities;
using Microsoft.AspNetCore.Mvc;
using WebApi.ApiConfig;
using WebApi.Models.Funcionario;

namespace WebApi.Controllers
{
    public class LoginController : Controller
    {
        private readonly IFuncionarioService _funcionario;
        private readonly IMapper mapper;
        public LoginController(IFuncionarioService funcionario, IMapper mapper)
        {
            this._funcionario = funcionario;
            this.mapper = mapper;
        }
        [HttpGet("Login")]
        public IActionResult Index()
        {
            return Ok();
        }
        [HttpPost("Logar")]
        public async Task<IActionResult> Logar(FuncionarioLoginViewModel funcionarioLogin)
        {
            Funcionario funcionario = mapper.Map<Funcionario>(funcionarioLogin);
            SingleResponse<Funcionario> singleResponse = await _funcionario.GetLogin(funcionario);
            if (!singleResponse.HasSuccess)
            {
                return BadRequest();
            }
            string token = TokenService.GenerateToken(singleResponse.Item);
            funcionario.Senha = "";

            return Ok(token);
        }
    }
}
