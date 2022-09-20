using AutoMapper;
using BLL.Interfaces;
using Common;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApi.Models.Funcionario;
using WebApi.Models.Token;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("{controller}")]
    [AllowAnonymous]
    public class LoginController : Controller
    {
        private readonly IFuncionarioService _funcionario;
        private readonly IMapper mapper;
        private readonly ITokenService tokenService;
        public LoginController([FromBody] IFuncionarioService funcionario, IMapper mapper, ITokenService service)
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
        public async Task<IActionResult> Logar([FromBody] FuncionarioLoginViewModel funcionarioLogin)
        {
            Funcionario funcionario = mapper.Map<Funcionario>(funcionarioLogin);
            SingleResponse<Funcionario> singleResponse = await _funcionario.GetLogin(funcionario);
            if (!singleResponse.HasSuccess)
            {
                return BadRequest(singleResponse.Message);
            }
            SingleResponse<string> token = tokenService.GenerateToken(funcionario);
            SingleResponse<string> refreshToken = tokenService.RefreshToken();
            SingleResponse<Funcionario> response = await tokenService.InsertRefreshToken(funcionario.Email,refreshToken.Item);
            if (!response.HasSuccess)
            {
                return BadRequest(response);
            }
            return Ok(new FuncionarioLoginViewModel
            {
                Email = response.Item.Email,
                Senha = response.Item.Senha,
                Token = token.Item,
                RefreshToken = response.Item.RefreshToken
            });
        }
        [HttpPost("Refresh")]
        public async Task<IActionResult> Refresh([FromBody] TokenViewModel viewModel)
        {
            SingleResponse<ClaimsPrincipal> principal = tokenService.GetPrincipalFromExpiredToken(viewModel.Token);
            string email = principal.Item.Identity.Name;
            SingleResponse<Funcionario> savedRefreshToken = await tokenService.GetRefreshToken(email);
            if (!savedRefreshToken.Item.RefreshToken.Equals(viewModel.RefreshToken))
            {
                return BadRequest(savedRefreshToken.Message = "O token salvo não é igual ao token atual");
            }
            SingleResponse<string> newJwtToken = tokenService.GenerateToken(principal.Item.Claims);
            if (!newJwtToken.HasSuccess)
            {
                return BadRequest(newJwtToken.Message = "Erro na geração de um token");
            }
            SingleResponse<string> newRefreshToken = tokenService.RefreshToken();
            if (!newRefreshToken.HasSuccess)
            {
                return BadRequest(newRefreshToken.Message = "Erro na geração de refresh token");
            }
            Response response = await tokenService.DeleteRefreshToken(email, newRefreshToken.Item);
            if (response.HasSuccess)
            {
                SingleResponse<Funcionario> newRToken = await tokenService.InsertRefreshToken(email, newRefreshToken.Item);
                if (!newRToken.HasSuccess)
                {
                    return BadRequest(newRToken.Message = "Erro na hora de salvar o novo Refresh Token");
                }
              
                return Ok(new AuthenticateResponse
                {
                    Token = newJwtToken.Item,
                    Refresh = newRToken.Item.RefreshToken,
                });

            }
            return BadRequest(response.Message = "Não foi possível deletar o token");
        }
    }
}
