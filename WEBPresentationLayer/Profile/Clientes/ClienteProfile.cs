using Entities;
using WEBPresentationLayer.Models.Cliente;

namespace WEBPresentationLayer.Profile.Clientes
{
    public class ChamadosProfile : AutoMapper.Profile
    {
        public ChamadosProfile()
        {
            CreateMap<ClienteInsertViewModel, Cliente>();
            CreateMap<ClienteSelectViewModel, Cliente>();
            CreateMap<Cliente, ClienteSelectViewModel>();
            CreateMap<Cliente, ClienteUpdateViewModel> ();
            CreateMap<ClienteUpdateViewModel, Cliente> ();
        }
    }
}
