using Entities;
using WEBPresentationLayer.Models.Funcionarios;

namespace WEBPresentationLayer.Profile.Funcionarios
{
    public class FuncionariosProfile : AutoMapper.Profile
    {
        public FuncionariosProfile()
        {
            CreateMap<FuncionariosInsertViewModel, Funcionario>();
            CreateMap<FuncionarioSelectViewModel, Funcionario>();
            CreateMap<Funcionario, FuncionarioSelectViewModel>();
            CreateMap<Funcionario, FuncionarioUpdateViewModel> ();
            CreateMap<FuncionarioUpdateViewModel, Funcionario> ();
        }
    }
}
