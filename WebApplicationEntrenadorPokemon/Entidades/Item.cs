namespace WebApplicationEntrenadorPokemon.Entidades
{
    public class Item
    {
        public int Id { get; set; }
        public int EntrenadorId { get; set; }
        public List<Entrenador> Entrenador { get; set; }
        public int ItemId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Sprite { get; set; }
        public DateTime FechaCreacion { get; set; }

    }
}
