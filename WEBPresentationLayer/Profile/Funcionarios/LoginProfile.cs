using Entities;
using WEBPresentationLayer.Models.Funcionario;

namespace WEBPresentationLayer.Profile.Funcionarios
{
    public class SenhaProfile : AutoMapper.Profile
    {
        public SenhaProfile()
        {
            CreateMap<FuncionarioLoginViewModel, Funcionario>();
            CreateMap<Funcionario, FuncionarioLoginViewModel>();
        }
    }
}
