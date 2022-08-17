using Entities;
using WEBPresentationLayer.Models.Cliente;

namespace WEBPresentationLayer.Profile.Clientes
{
    public class ClienteProfile : AutoMapper.Profile
    {
        public ClienteProfile()
        {
            CreateMap<ClienteInsertViewModel, Cliente>(); 
        }
    }
}
