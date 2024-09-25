using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebApplicationEntrenadorPokemon.DTOs;
using WebApplicationEntrenadorPokemon.Entidades;

namespace WebApplicationEntrenadorPokemon.Controllers
{
    [Route("api/item")]
    [ApiController]
    public class ItemController : Controller
    {
        private readonly ApplicationDBContext context;

        public ItemController(ApplicationDBContext context)
        {
            this.context = context;
        }


        [HttpPost("item")]
        public async Task<IActionResult> Item([FromBody] creaItemDTO request)
        {
            var entrenadorId = Convert.ToInt32(User.FindFirst(ClaimTypes.Email)?.Value);

            var item = new Item
            {
                EntrenadorId = entrenadorId,
                ItemId = request.id,
                Nombre = request.nombre,
                Descripcion = request.descripcion,
                Sprite = request.imagen,
                FechaCreacion = DateTime.UtcNow
            };


            context.Add(item);
            await context.SaveChangesAsync();


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
            var resultado = await context.Item.FirstOrDefaultAsync(x => x.ItemId == id && x.EntrenadorId == entrenadorId);

            if (resultado is null)
            {
                return NotFound();
            }

            context.Remove(resultado);
            await context.SaveChangesAsync();



            var entidad = await itemsAgrupadosCantidad(entrenadorId);

            return entidad;

        }



        private async Task<List<itemDTO>> itemsAgrupadosCantidad(int entrenadorId)
        {

            var entidad = await context.Item.Where(x => x.EntrenadorId == entrenadorId)
               .GroupBy(i => new { i.ItemId, i.Nombre, i.Descripcion, i.Sprite })
               .Select(a => new itemDTO
               {

                   itemId = a.Key.ItemId,
                   nombre = a.Key.Nombre,
                   descripcion = a.Key.Descripcion,
                   sprite = a.Key.Sprite,
                   TotalItemId = a.Count()

               }).ToListAsync();

            return entidad;
        }

    }
}
