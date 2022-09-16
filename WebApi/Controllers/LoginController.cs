using AutoMapper;
using BLL.Interfaces;
using Common;
using Entities;
using Microsoft.AspNetCore.Mvc;
using WebApi.ApiConfig;
using WebApi.Interface;
using WebApi.Models.Funcionario;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("{controller}")]
    public class LoginController : Controller
    {
        private readonly IFuncionarioService _funcionario;
        private readonly IMapper mapper;
        private readonly ITokenService tokenService;
        public LoginController(IFuncionarioService funcionario, IMapper mapper, ITokenService service)
        {
            _funcionario = funcionario;
            this.mapper = mapper;
            tokenService = service;
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
            SingleResponse<string> token = tokenService.GenerateToken(singleResponse.Item);
            funcionario.Senha = "";

            return Ok(token);
        }
    }
}
