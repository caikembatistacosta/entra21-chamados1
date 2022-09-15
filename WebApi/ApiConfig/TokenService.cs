using Common;
using Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApi.Interface;

namespace WebApi.ApiConfig
{
    public class TokenService : ITokenService
    {

        public SingleResponse<string> GenerateToken(Funcionario funcionario)
        {
            JwtSecurityTokenHandler tokenHandler = new();
            byte[] key = Encoding.ASCII.GetBytes(Settings.Secret);
            SecurityTokenDescriptor tokenDescriptor = new()
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Email, funcionario.Email),
                    new Claim(ClaimTypes.Role, funcionario.NivelDeAcesso.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            if (token == null)
            {
                SingleResponse<string> singles = new()
                {
                    HasSuccess = false,
                    Message = "Token Não foi gerado"
                };
                return singles;
            }
            SingleResponse<string> single = new()
            {
                HasSuccess = true,
                Message = "Token foi gerado com sucesso",
                Item = tokenHandler.WriteToken(token)
            };
            return single;
        }
    }
}
