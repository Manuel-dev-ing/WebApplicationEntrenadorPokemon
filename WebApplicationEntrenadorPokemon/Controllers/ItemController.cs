using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebApplicationEntrenadorPokemon.DTOs;
using WebApplicationEntrenadorPokemon.Entidades;
using WebApplicationEntrenadorPokemon.Servicios;

namespace WebApplicationEntrenadorPokemon.Controllers
{
    [Route("api/item")]
    [ApiController]
    public class ItemController : Controller
    {
        private readonly IRepositorioItems repositorioItems;

        public ItemController(IRepositorioItems repositorioItems)
        {
            this.repositorioItems = repositorioItems;
        }


        [HttpPost("item")]
        public async Task<IActionResult> Item([FromBody] creaItemDTO request)
        {
            var entrenadorId = Convert.ToInt32(User.FindFirst(ClaimTypes.Email)?.Value);

            var resultado = await repositorioItems.guardarItem(request, entrenadorId);

            if (resultado == "Noguardado")
            {
                return NotFound();
            }


            return Created();
        }


        [HttpGet("misItems")]
        public async Task<List<itemDTO>> misItems()
        {

            var entrenadorId = Convert.ToInt32(User.FindFirst(ClaimTypes.Email)?.Value);

            var entidad = await itemsAgrupadosCantidad(entrenadorId);

            return entidad;
        }


        [HttpDelete("{id:int}")]
        public async Task<ActionResult<List<itemDTO>>> delete(int id)
        {
            var entrenadorId = Convert.ToInt32(User.FindFirst(ClaimTypes.Email)?.Value);

            var resultado = await repositorioItems.eliminarItems(entrenadorId, id);

            if (resultado == "notFound" || resultado == "Noguardado")
            {
                return NotFound();
            }


            var entidad = await itemsAgrupadosCantidad(entrenadorId);

            return entidad;

        }



        private async Task<List<itemDTO>> itemsAgrupadosCantidad(int entrenadorId)
        {

            var entidad = await repositorioItems.itemsAgrupadosCantidad(entrenadorId);

            return entidad;
        }

    }
}
