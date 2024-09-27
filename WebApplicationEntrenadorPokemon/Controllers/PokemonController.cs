using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Security.Claims;
using WebApplicationEntrenadorPokemon.DTOs;
using WebApplicationEntrenadorPokemon.Entidades;
using WebApplicationEntrenadorPokemon.Servicios;

namespace WebApplicationEntrenadorPokemon.Controllers
{
    [Route("api/pokemon")]
    [ApiController]
    public class PokemonController : Controller
    {
        private readonly IRepositorioPokemon repositorioPokemon;

        public PokemonController(IRepositorioPokemon repositorioPokemon)
        {
            this.repositorioPokemon = repositorioPokemon;
        }


        [HttpGet]
        public async Task<List<tipoPokemonDTO>> Get()
        {

            var entidad = await repositorioPokemon.obtenerListadoTiposPokemon();

            return entidad;

        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] crearPokemonDTO request)
        {

            var entrenadorId = Convert.ToInt32(User.FindFirst(ClaimTypes.Email)?.Value);

            var resultado = await repositorioPokemon.guardarPokemon(request, entrenadorId);

            if (resultado == "400")
            {
                var mensaje = "No puedes guardar mas de 6 pokemon";
                var code = "400";
                return BadRequest(new { mensaje = mensaje, code = code});
            }

            if (resultado == "Noguardado")
            {
                return BadRequest();
            }

            return Created();

        }


        [HttpGet("pok")]
        public async Task<List<PokemonDTO>> Pokemon()
        {
            var entrenadorId = Convert.ToInt32(User.FindFirst(ClaimTypes.Email)?.Value);

            var entidad = await repositorioPokemon.GetPokemonsAsync(entrenadorId);


            return entidad;
            
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> delete(int id)
        {
            var entrenadorId = Convert.ToInt32(User.FindFirst(ClaimTypes.Email)?.Value);

            var resultado = await repositorioPokemon.elimnarPokemon(entrenadorId, id);

            if (resultado == "404")
            {
                return NotFound();
            }

            
            return Ok();

        }




    }
}
