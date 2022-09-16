using AutoMapper;
using BLL.Impl;
using BLL.Interfaces;
using Common;
using Entities;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models.Cliente;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/{controller}/{id}")]

    public class ClienteController : Controller
    {
        private readonly IClienteService _clientesvc;
        private readonly IMapper _mapper;

        public ClienteController(IClienteService svc, IMapper mapper)
        {
            this._clientesvc = svc;
            this._mapper = mapper;
        }
        [HttpGet("All-Costumers")]
        public async Task<IActionResult> Index()
        {

            DataResponse<Cliente> responseClientes = await _clientesvc.GetAll();

            if (!responseClientes.HasSuccess)
            {
                return BadRequest();
            }


            List<ClienteSelectViewModel> clientes = _mapper.Map<List<ClienteSelectViewModel>>(responseClientes.Data);
            return Ok(clientes);
        }
        [HttpGet("Insert-Costumer")]
        public IActionResult Create()
        {
            return Ok();
        }

        [HttpPost("Insert-Costumer")]
        public async Task<IActionResult> Create(ClienteInsertViewModel viewModel)
        {

            Cliente cliente = _mapper.Map<Cliente>(viewModel);

            Response response = await _clientesvc.Insert(cliente);

            if (response.HasSuccess)
                return Ok(response);

            return BadRequest(response.Message);
        }
        [HttpGet("Edit-Costumer")]
        public async Task<IActionResult> Edit(int id)
        {
            SingleResponse<Cliente> responseCliente = await _clientesvc.GetById(id);
            if (!responseCliente.HasSuccess)
            {
                
                return BadRequest(responseCliente.Message);
            }
            Cliente cliente = responseCliente.Item;
            ClienteUpdateViewModel updateViewModel = _mapper.Map<ClienteUpdateViewModel>(cliente);
            return Ok(updateViewModel);

        }
        [HttpPut("Edit-Costumer")]
        public async Task<IActionResult> Edit(ClienteUpdateViewModel viewModel)
        {
            Cliente cliente = _mapper.Map<Cliente>(viewModel);
            Response response = await _clientesvc.Update(cliente);
            if (response.HasSuccess)
            {
                return BadRequest(response.Message);
            }
            ViewBag.Errors = response.Message;
            return Ok(cliente);
        }

        //public IActionResult Delete()
        //{
        //    return View();
        //}

    }
}
