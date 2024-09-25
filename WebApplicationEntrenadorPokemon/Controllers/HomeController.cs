using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;
using WebApplicationEntrenadorPokemon.Entidades;
using WebApplicationEntrenadorPokemon.Models;
using WebApplicationEntrenadorPokemon.Servicios;

namespace WebApplicationEntrenadorPokemon.Controllers
{
    public class HomeController : Controller
    {
        
        private readonly IRepositorioPokemon repositorioPokemon;

        public HomeController(IRepositorioPokemon repositorioPokemon)
        {
            
            this.repositorioPokemon = repositorioPokemon;
        }

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult misPoekmon()
        {

            return View();
        }

        public IActionResult misItems()
        {

            return View();
        }

        public IActionResult Items()
        {

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
