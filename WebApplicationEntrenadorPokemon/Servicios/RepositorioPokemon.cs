using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebApplicationEntrenadorPokemon.DTOs;
using WebApplicationEntrenadorPokemon.Entidades;
using WebApplicationEntrenadorPokemon.Models;

namespace WebApplicationEntrenadorPokemon.Servicios
{
    public interface IRepositorioPokemon
    {
        Task<string> elimnarPokemon(int entrenadorId, int id);
        Task<List<PokemonDTO>> GetPokemonsAsync(int id);
        Task<string> guardarPokemon(crearPokemonDTO request, int entrenadorId);
        Task<List<tipoPokemonDTO>> obtenerListadoTiposPokemon();
    }



    public class RepositorioPokemon: IRepositorioPokemon
    {
        private readonly ApplicationDBContext context;

        public RepositorioPokemon(ApplicationDBContext context)
        {
            this.context = context;
        }


        public async Task<List<tipoPokemonDTO>> obtenerListadoTiposPokemon()
        {
            var entidad = await context.TiposPokemon.Select(tipo => new tipoPokemonDTO
            {
                id = tipo.Id,
                tipoPokemon = tipo.tipoPokemon
            }).ToListAsync();

            return entidad;
        }

        public async Task<string> guardarPokemon(crearPokemonDTO request, int entrenadorId)
        {

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
                var badRequest = "400";
                return badRequest;
                //var mensaje = "No puedes guardar mas de 6 pokemon";
                //var code = "400";

                //var list = new { mensaje = mensaje, code = "400" };

                //List<string> lista = new List<string>();
                //lista.Add(mensaje);
                //lista.Add(code);

                
            }


            var pok = entidad;

            context.Add(pokemon);
            int resultado = await context.SaveChangesAsync();
            var estaGuardado = resultado > 0 ? "guardado" : "Noguardado";


            return estaGuardado;

        }

        public async Task<string> elimnarPokemon(int entrenadorId, int id)
        {
            var entidad = await context.Pokemon.FirstOrDefaultAsync(x => x.id == id && x.EntrenadorId == entrenadorId);

            if (entidad is null)
            {
                var notFound = "404";
                return notFound;
            }

            context.Remove(entidad);
            int resultado = await context.SaveChangesAsync();
            var estaEliminado = resultado > 0 ? "eliminado" : "NoEliminado";
            return estaEliminado;

        }


        public async Task<List<PokemonDTO>> GetPokemonsAsync(int id)
        {
            var entidad = await context.Pokemon.Where(x => x.EntrenadorId == id).Select(a => new PokemonDTO
            {
                id = a.id,
                EntrenadorId = a.EntrenadorId,
                nombre = a.nombre,
                tipo = a.tipo,
                altura = a.altura,
                peso = a.peso,
                officialArtwork = a.officialArtwork

            }).ToListAsync();

            return entidad;
        }





    }
}
