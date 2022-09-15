using Common;
using Entities;
using WebApi.ApiConfig;

namespace WebApi.Interface
{
    public interface ITokenService
    {
        SingleResponse<string> GenerateToken(Funcionario funcionario);
    }
}
