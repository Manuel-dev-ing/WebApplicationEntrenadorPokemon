using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using WebApplicationEntrenadorPokemon.Entidades;

namespace WebApplicationEntrenadorPokemon.Models
{
    public class RegistroViewModel
    {
        [Required(ErrorMessage = "El campo ID Entrenador es requerido")]
        [Display(Name = "ID Entrenador")]
        public string IDEntrenador { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El campo Nombre es requerido")]
        public string Nombre { get; set; }

        [Range(1, maximum: int.MaxValue, ErrorMessage = "Debe seleccionar un Genero")]
        [Display(Name = "Genero")]
        [Required(ErrorMessage = "El campo Genero es requerido")]
        public int IDGenero { get; set; }
        public IEnumerable<SelectListItem> tiposGenero { get; set; }


        [Required(ErrorMessage = "El campo Contraseña es requerido")]
        [Display(Name = "Contraseña")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
