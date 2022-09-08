using Entities;
using WEBApi.Models.Chamado;

namespace WEBApi.Profile.Chamados
{
    public class ChamadoProfile : AutoMapper.Profile
    {
        public ChamadoProfile()
        {
            CreateMap<ChamadoInsertViewModel, Chamado>();
            CreateMap<ChamadoSelectViewModel, Chamado>();
            CreateMap<ChamadoUpdateViewModel, Chamado>();
            CreateMap<Chamado, ChamadoUpdateViewModel>();
            CreateMap<Chamado, ChamadoSelectViewModel>();
            CreateMap<ChamadoDetailsViewModel, Chamado>();
            CreateMap<Chamado, ChamadoDetailsViewModel>();
        }
    }
}
