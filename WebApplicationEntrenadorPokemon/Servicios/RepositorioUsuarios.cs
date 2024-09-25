using Microsoft.EntityFrameworkCore;
using WebApplicationEntrenadorPokemon.Entidades;

namespace WebApplicationEntrenadorPokemon.Servicios
{
    public interface IRepositorioUsuarios
    {
        Task<Entrenador> BuscarEntrenadorPorId(string identrenador);
        Task<Entrenador> BuscarPorId(int id);
        Task CrearEntrenador(Entrenador entrenador);
        Task<IEnumerable<Genero>> obtenerGeneros();
    }


    public class RepositorioUsuarios: IRepositorioUsuarios
    {
        private readonly ApplicationDBContext context;

        public RepositorioUsuarios(ApplicationDBContext context)
        {
            this.context = context;
        }

        public async Task CrearEntrenador(Entrenador entrenador)
        {
            context.Add(entrenador);
            await context.SaveChangesAsync();
        }
        
        public async Task<Entrenador> BuscarPorId(int id)
        {
            var entidad = await context.Entrenador.FirstOrDefaultAsync(x => x.Id == id);
            return entidad;
        
        }

        public async Task<Entrenador> BuscarEntrenadorPorId(string identrenador)
        {
            var entidad = await context.Entrenador.FirstOrDefaultAsync(x => x.EntrenadorId == identrenador);
            return entidad;
        }


        public async Task<IEnumerable<Genero>> obtenerGeneros()
        {
            var entidad = context.Generos.ToList();
            return entidad;

        }
    }
}
