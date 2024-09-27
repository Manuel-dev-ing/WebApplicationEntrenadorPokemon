using Microsoft.EntityFrameworkCore;
using WebApplicationEntrenadorPokemon.DTOs;
using WebApplicationEntrenadorPokemon.Entidades;

namespace WebApplicationEntrenadorPokemon.Servicios
{
    public interface IRepositorioItems
    {
        Task<string> eliminarItems(int entrenadorId, int id);
        Task<string> guardarItem(creaItemDTO request, int entrenadorId);
        Task<List<itemDTO>> itemsAgrupadosCantidad(int entrenadorId);
    }


    public class RepositorioItems: IRepositorioItems
    {
        private readonly ApplicationDBContext context;

        public RepositorioItems(ApplicationDBContext context)
        {
            this.context = context;
        }

        public async Task<string> guardarItem(creaItemDTO request, int entrenadorId)
        {
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
            var resultado = await context.SaveChangesAsync();

            var estaGuardado = resultado > 0 ? "guardado" : "Noguardado";

            return estaGuardado;
        }


        public async Task<string> eliminarItems(int entrenadorId, int id)
        {

            var resultado = await context.Item.FirstOrDefaultAsync(x => x.ItemId == id && x.EntrenadorId == entrenadorId);

            if (resultado is null)
            {
                var notFound = "notFound";

                return notFound;
            }

            context.Remove(resultado);
            var entidad = await context.SaveChangesAsync();

            var estaEliminado = entidad > 0 ? "eliminado" : "Noeliminado";

            return estaEliminado;
        }

        public async Task<List<itemDTO>> itemsAgrupadosCantidad(int entrenadorId)
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
