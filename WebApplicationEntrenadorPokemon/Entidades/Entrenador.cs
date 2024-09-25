namespace WebApplicationEntrenadorPokemon.Entidades
{
    public class Entrenador
    {

        public int Id { get; set; }
        public string EntrenadorId { get; set; }
        public string Nombre { get; set; }
        public int GeneroId { get; set; }
        public List<Genero> Genero { get; set; }
        public string Password { get; set; }
        public Pokemon Pokemon { get; set; }

    }
}
