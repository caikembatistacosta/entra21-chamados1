using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WEBPresentationLayer.Models;

namespace WEBPresentationLayer.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
            
        }
        public IActionResult Index()
        {
            string _usario;
            bool _autenticado;
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                _usario = HttpContext.User.Identity.Name;
                _autenticado = true;
            }
            else
            {
                _usario = "Não Logado";
                _autenticado = false;
            }
            ViewBag.Usuario = _usario;
            ViewBag.Autenticado = _autenticado;
            return View();
        }


    }
}