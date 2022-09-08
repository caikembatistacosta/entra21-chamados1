using AutoMapper;
using BLL.Interfaces;
using Common;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WEBPresentationLayer.Models.Chamado;

namespace WEBPresentationLayer.Controllers
{
    public class ChamadoController : Controller
    {
        private readonly IChamadoService _chamadosvc;
        private readonly IMapper _mapper;
        public ChamadoController(IChamadoService svc, IMapper mapper)
        {
            this._chamadosvc = svc;
            this._mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {

            DataResponse<Chamado> responseChamados = await _chamadosvc.GetAll();

            if (!responseChamados.HasSuccess)
            {
                ViewBag.Errors = responseChamados.Message;
                return View();
            }
            List<ChamadoSelectViewModel> chamados = _mapper.Map<List<ChamadoSelectViewModel>>(responseChamados.Data);
            return View(chamados);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ChamadoInsertViewModel viewModel)
        {
            HttpClient client = new HttpClient();
            string data = JsonConvert.SerializeObject(viewModel);
            StringContent content = new StringContent(data);

            HttpResponseMessage message = await client.PostAsync("localhost:5000/Chamado/InsertChamado", content);
            return View(message);
            

           
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            SingleResponse<Chamado> responseChamado = await _chamadosvc.GetById(id);
            if (!responseChamado.HasSuccess)
            {
                return RedirectToAction(nameof(Index));
            }
            Chamado chamado= responseChamado.Item;
            ChamadoUpdateViewModel updateViewModel = _mapper.Map<ChamadoUpdateViewModel>(chamado);
            return View(updateViewModel);

        }
        [HttpPost]
        public async Task<IActionResult> Edit(ChamadoUpdateViewModel viewModel)
        {
            Chamado chamado = _mapper.Map<Chamado>(viewModel);
            Response response = await _chamadosvc.Update(chamado);
            if (response.HasSuccess)
            {
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Errors = response.Message;
            return View(chamado);
        }
        public async Task<IActionResult> Details(int id)
        {

            var chamado = new Chamado();
            ChamadoDetailsViewModel viewModel = _mapper.Map<ChamadoDetailsViewModel>(chamado);
            return View(viewModel);
        }
        

    }
}
