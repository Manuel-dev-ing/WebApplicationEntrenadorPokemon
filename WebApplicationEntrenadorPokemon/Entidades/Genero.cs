﻿namespace WebApplicationEntrenadorPokemon.Entidades
{
    public class Genero
    {

        public int Id { get; set; }
        public string Nombre { get; set; }

        public Entrenador Entrenador { get; set; }
    }
}
