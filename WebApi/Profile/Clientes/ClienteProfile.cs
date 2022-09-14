using Entities;
using WebApi.Models.Cliente;

namespace WEBApi.Profile.Clientes
{
    public class ClientesProfile : AutoMapper.Profile
    {
        public ClientesProfile()
        {
            CreateMap<ClienteSelectViewModel, Cliente>();
            CreateMap<Cliente, ClienteSelectViewModel>();
            CreateMap<Cliente, ClienteUpdateViewModel> ();
            CreateMap<ClienteUpdateViewModel, Cliente>();
            CreateMap<Cliente, ClienteInsertViewModel>();
            CreateMap<ClienteInsertViewModel, Cliente>();

        }
    }
}
