using System.ComponentModel.DataAnnotations;

namespace WebApplicationEntrenadorPokemon.Models
{
    public class LoginViewModel
    {
        [Display(Name = "ID Entrenador")]
        [Required(ErrorMessage = "El campo ID Entrenador es requerido")]
        public string IDEntrenador { get; set; }

        [Display(Name = "Contraseña")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "El campo Contraseña es requerido")]
        public string Password { get; set; }

        public bool Recuerdame { get; set; }

    }
}
