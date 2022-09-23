using Common;
using Entities;

namespace BLL.Interfaces
{
    public interface IFuncionarioService
    {
        Task<Response> Insert(Funcionario funcionario);
        Task<Response> Update(Funcionario funcionario);
        Task<Response> Delete(Funcionario funcionario);
        Task<DataResponse<Funcionario>> GetAll();
        Task<SingleResponse<Funcionario>> GetById(int id);
        Task<SingleResponse<Funcionario>> GetLogin(Funcionario funcionario);
    }
}
