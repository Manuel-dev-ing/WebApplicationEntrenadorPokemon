using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using WebApplicationEntrenadorPokemon.Entidades;
using WebApplicationEntrenadorPokemon.Models;
using WebApplicationEntrenadorPokemon.Servicios;

namespace WebApplicationEntrenadorPokemon.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly IRepositorioUsuarios repositorioUsuarios;
        private readonly UserManager<Entrenador> userManager;
        private readonly SignInManager<Entrenador> signInManager;

        public UsuariosController(IRepositorioUsuarios repositorioUsuarios, UserManager<Entrenador> userManager, SignInManager<Entrenador> signInManager)
        {
            this.repositorioUsuarios = repositorioUsuarios;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();        
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel modelo)
        {
            if (!ModelState.IsValid)
            {
                return View(modelo);
            }

            var resultado = await signInManager.PasswordSignInAsync(modelo.IDEntrenador, modelo.Password, modelo.Recuerdame, lockoutOnFailure: false);

            var entrenador = await userManager.FindByNameAsync(modelo.IDEntrenador);

            if (resultado.Succeeded)
            {
               /* var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.GivenName, entrenador.Nombre),
                    new Claim(ClaimTypes.HomePhone, entrenador.EntrenadorId),

                };

                await userManager.AddClaimsAsync(entrenador, claims);*/

                return RedirectToAction("Index", "Home");


            }
            else
            {
                ModelState.AddModelError(String.Empty, "ID de Entrenador o password incorrecto");
                return View(modelo);
            }


        }


        [HttpGet]
        public async Task<ActionResult> Registro()
        {
            var modelo = new RegistroViewModel();
            modelo.tiposGenero = await obtenerrGeneros();

            
            return View(modelo);
        }

        [HttpPost]
        public async Task<IActionResult> Registro(RegistroViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);

            }

            var entrenador = new Entrenador
            {
                EntrenadorId = model.IDEntrenador,
                Nombre = model.Nombre,
                GeneroId = model.IDGenero,
            };

            var resultado = await userManager.CreateAsync(entrenador, password: model.Password);
            /*var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.GivenName, entrenador.Nombre),
                new Claim(ClaimTypes.HomePhone, entrenador.EntrenadorId),
            };*/

            if (resultado.Succeeded)
            {
               /* await userManager.AddClaimsAsync(entrenador, claims);*/
                await signInManager.SignInAsync(entrenador, isPersistent: true);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError(String.Empty, "Error al registrar entrenador");
                return View(model);
            }

        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
            return RedirectToAction("Login", "Usuarios");
        }

        private async Task<IEnumerable<SelectListItem>> obtenerrGeneros()
        {
            var generos = await repositorioUsuarios.obtenerGeneros();
            var resultado = generos.Select(x => new SelectListItem(x.Nombre, x.Id.ToString())).ToList();

            var opcionePorDefecto = new SelectListItem("-- Selecciona un Genero --", "0", true);
            resultado.Insert(0, opcionePorDefecto);
            return resultado;
        }


    }
}
