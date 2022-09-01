﻿using Entities;
using WEBPresentationLayer.Models.Cliente;

namespace WEBPresentationLayer.Profile.Clientes
{
    public class ChamadosProfile : AutoMapper.Profile
    {
        public ChamadosProfile()
        {
            CreateMap<ChamadoInsertViewModel, Cliente>();
            CreateMap<ChamadoSelectViewModel, Cliente>();
            CreateMap<Cliente, ChamadoSelectViewModel>();
            CreateMap<Cliente, ClienteUpdateViewModel> ();
            CreateMap<ClienteUpdateViewModel, Cliente> ();
        }
    }
}