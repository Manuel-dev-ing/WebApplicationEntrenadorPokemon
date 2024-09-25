using WebApplicationEntrenadorPokemon.Entidades;

namespace WebApplicationEntrenadorPokemon.DTOs
{
    public class itemDTO
    {
        public int itemId { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public string sprite { get; set; }
        public int TotalItemId { get; set; }
    }
}
