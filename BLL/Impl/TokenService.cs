﻿using BLL.Interfaces;
using Common;
using DAO.Interfaces;
using Entities;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Impl
{
    public class TokenService : ITokenService
    {
        private readonly ITokenDAO tokenDAO;
        public TokenService(ITokenDAO tokenDAO)
        {
            this.tokenDAO = tokenDAO;
        }

        public Task<Response> DeleteRefreshToken(string email, string refreshToken)
        {
            return tokenDAO.DeleteRefreshToken(email,refreshToken);
        }
        public Task<SingleResponse<Funcionario>> GetRefreshToken(string email)
        {
            return tokenDAO.GetRefreshToken(email);
        }

        public Task<SingleResponse<Funcionario>> InsertRefreshToken(string email, string refreshToken)
        {
            return tokenDAO.InsertRefreshToken(email,refreshToken);
        }
        public SingleResponse<string> GenerateToken(Funcionario funcionario)
        {
            JwtSecurityTokenHandler tokenHandler = new();
            byte[] key = Encoding.ASCII.GetBytes(Settings.Secret);
            SecurityTokenDescriptor tokenDescriptor = new()
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, funcionario.Email),
                    new Claim(ClaimTypes.Role, funcionario.NivelDeAcesso.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            if (token == null)
            {
                return SingleResponseFactory<string>.CreateInstance().CreateFailureSingleResponse();
            }
            return SingleResponseFactory<string>.CreateInstance().CreateSuccessSingleResponse(tokenHandler.WriteToken(token));
        }

        public SingleResponse<string> GenerateToken(IEnumerable<Claim> claims)
        {
            JwtSecurityTokenHandler tokenHandler = new();
            byte[] key = Encoding.ASCII.GetBytes(Settings.Secret);
            SecurityTokenDescriptor tokenDescriptor = new()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            if (token == null)
            {
                return SingleResponseFactory<string>.CreateInstance().CreateFailureSingleResponse();
            }
            return SingleResponseFactory<string>.CreateInstance().CreateSuccessSingleResponse(tokenHandler.WriteToken(token));
        }

        public SingleResponse<ClaimsPrincipal> GetPrincipalFromExpiredToken(string token)
        {
            try
            {
                TokenValidationParameters tokenValidationParams = new()
                {
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Settings.Secret.PadRight((512/8),'\0'))),
                    ValidateLifetime = false,
                };
                JwtSecurityTokenHandler tokenHandler = new();
                ClaimsPrincipal principal = tokenHandler.ValidateToken(token, tokenValidationParams, out var securityToken);
                if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                {
                    return SingleResponseFactory<ClaimsPrincipal>.CreateInstance().CreateFailureSingleResponse("Token inválido");
                }
                return SingleResponseFactory<ClaimsPrincipal>.CreateInstance().CreateSuccessSingleResponse(principal);
            }
            catch (Exception ex)
            {
                return SingleResponseFactory<ClaimsPrincipal>.CreateInstance().CreateFailureSingleResponse(ex);
            }
            
        }
        public SingleResponse<string> RefreshToken()
        {
            byte[] randomNumber = new byte[32];
            using RandomNumberGenerator rgn = RandomNumberGenerator.Create();
            rgn.GetBytes(randomNumber);
            return SingleResponseFactory<string>.CreateInstance().CreateSuccessSingleResponse(Convert.ToBase64String(randomNumber));
        }
    }
}
