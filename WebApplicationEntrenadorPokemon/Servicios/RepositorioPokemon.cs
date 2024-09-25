using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebApplicationEntrenadorPokemon.DTOs;
using WebApplicationEntrenadorPokemon.Entidades;
using WebApplicationEntrenadorPokemon.Models;

namespace WebApplicationEntrenadorPokemon.Servicios
{
    public interface IRepositorioPokemon
    {
        Task<List<PokemonDTO>> GetPokemonsAsync(int id);
    }



    public class RepositorioPokemon: IRepositorioPokemon
    {
        private readonly ApplicationDBContext context;

        public RepositorioPokemon(ApplicationDBContext context)
        {
            this.context = context;
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
