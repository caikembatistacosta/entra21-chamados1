using AutoMapper;
using BLL.Interfaces;
using Common;
using Entities;
using Microsoft.AspNetCore.Mvc;
using WEBApi.Models.Chamado;

namespace WEBApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChamadoController : Controller
    {
        private readonly IChamadoService _chamadoService;
        private readonly IMapper mapper;
        public ChamadoController(IChamadoService chamado, IMapper mapper)
        {
            _chamadoService = chamado;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult InsertChamado()
        {
            return Ok();
        }

        [HttpPost("InsertChamado")]
        public async Task<IActionResult> InsertChamado(ChamadoInsertViewModel chamado)
        {
            Chamado chamado2 = mapper.Map<Chamado>(chamado);

            Response response = await _chamadoService.Insert(chamado2);

            if (response.HasSuccess)
            {
                return BadRequest();
            }
            ViewBag.Errors = response.Message;
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateChamado(int id)
        {
            SingleResponse<Chamado> responseChamado = await _chamadoService.GetById(id);
            if (!responseChamado.HasSuccess)
            {
                return BadRequest();
            }
            Chamado chamado = responseChamado.Item;
            ChamadoUpdateViewModel updateViewModel = mapper.Map<ChamadoUpdateViewModel>(chamado);
            return Ok(updateViewModel);
        }

        [HttpPut("UpdateChamado")]
        public async Task<IActionResult> UpdateChamado(Chamado chamado)
        {
            //Chamado chamado = _mapper.Map<Chamado>(viewModel);
            Response response = await _chamadoService.Update(chamado);
            if (!response.HasSuccess)
            {
                return BadRequest();
            }
            return Ok(response);
        }
        [HttpPost("Details")]
        public async Task<IActionResult> Details(int id)
        {
            SingleResponse<Chamado> single = await _chamadoService.GetById(id);
            if (!single.HasSuccess)
            {
                return BadRequest();
            }
            Chamado chamado = single.Item;
            return Ok(chamado);
        }
    }
}
