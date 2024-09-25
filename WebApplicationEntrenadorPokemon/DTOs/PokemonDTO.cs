namespace WebApplicationEntrenadorPokemon.DTOs
{
    public class PokemonDTO
    {


        public int id { get; set; }

        public int EntrenadorId { get; set; }
        public string nombre { get; set; }
        public string tipo { get; set; }
        public int altura { get; set; }
        public int peso { get; set; }
        public string officialArtwork { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
