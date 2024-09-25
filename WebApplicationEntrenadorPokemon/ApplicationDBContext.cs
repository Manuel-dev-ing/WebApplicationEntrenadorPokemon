using Microsoft.EntityFrameworkCore;
using WebApplicationEntrenadorPokemon.Entidades;

namespace WebApplicationEntrenadorPokemon
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions options) : base(options)
        {
        }


        public DbSet<Genero> Generos { get; set; }
        public DbSet<TipoPokemon> TiposPokemon { get; set; }
        public DbSet<Pokemon> Pokemon { get; set; }
        public DbSet<Entrenador> Entrenador { get; set; }
        public DbSet<Item> Item { get; set; }

    }
}
