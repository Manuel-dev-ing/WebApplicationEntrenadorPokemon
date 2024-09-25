using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebApplicationEntrenadorPokemon.DTOs;
using WebApplicationEntrenadorPokemon.Entidades;

namespace WebApplicationEntrenadorPokemon.Controllers
{
    [Route("api/pokemon")]
    [ApiController]
    public class PokemonController : Controller
    {
        private readonly ApplicationDBContext context;

        public PokemonController(ApplicationDBContext context)
        {
            this.context = context;
        }


        [HttpGet]
        public async Task<List<tipoPokemonDTO>> Get()
        {
            var entidad = await context.TiposPokemon.Select(tipo => new tipoPokemonDTO
            {
                id = tipo.Id,
                tipoPokemon = tipo.tipoPokemon
            }).ToListAsync();

            return entidad;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] crearPokemonDTO request)
        {

            var entrenadorId = Convert.ToInt32(User.FindFirst(ClaimTypes.Email)?.Value);


            var pokemon = new Pokemon
            {
                EntrenadorId = entrenadorId,
                nombre = request.nombre,
                tipo = request.tipos,
                altura = request.altura,
                peso = request.peso,
                officialArtwork = request.imagen,
                FechaCreacion = DateTime.UtcNow
            };

            var entidad = context.Pokemon.Where(x => x.EntrenadorId == entrenadorId).Count();

            if (entidad >= 6)
            {
                var mensaje = "No puedes guardar mas de 6 pokemon";
                return BadRequest(new { mensaje = mensaje, code = "400" });
            }


            var pok = entidad;

            context.Add(pokemon);
            await context.SaveChangesAsync();


            return Created();
        }


        [HttpGet("pok")]
        public async Task<List<PokemonDTO>> Pokemon()
        {
            var entrenadorId = Convert.ToInt32(User.FindFirst(ClaimTypes.Email)?.Value);

            var entidad = await context.Pokemon.Where(x => x.EntrenadorId == entrenadorId).Select(a => new PokemonDTO
            {
                id = a.id,
                EntrenadorId = a.EntrenadorId,
                nombre = a.nombre,
                tipo = a.tipo,
                altura = a.altura,
                peso = a.peso,
                officialArtwork = a.officialArtwork,
                FechaCreacion = a.FechaCreacion

            }).ToListAsync();

            return entidad;
            //return Ok(new { mensaje = "OK MENSAJE POKEMON"});
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> delete(int id)
        {
            var entrenadorId = Convert.ToInt32(User.FindFirst(ClaimTypes.Email)?.Value);
            var resultado = await context.Pokemon.FirstOrDefaultAsync(x => x.id == id && x.EntrenadorId == entrenadorId);

            if (resultado is null)
            {
                return NotFound();
            }

            context.Remove(resultado);
            await context.SaveChangesAsync();

            return Ok();

        }




    }
}
