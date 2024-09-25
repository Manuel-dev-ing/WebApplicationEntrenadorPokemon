using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebApplicationEntrenadorPokemon.DTOs;
using WebApplicationEntrenadorPokemon.Entidades;
using WebApplicationEntrenadorPokemon.Servicios;

namespace WebApplicationEntrenadorPokemon.Controllers
{
    [Route("api/principal")]
    [ApiController]
    public class PrincipalController : ControllerBase
    {
        private readonly ApplicationDBContext context;

        public PrincipalController(ApplicationDBContext context)
        {
            this.context = context;
        }

       

        


        


        

       

        


    }
}
