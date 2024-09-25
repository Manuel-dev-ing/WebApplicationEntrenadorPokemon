using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebApplicationEntrenadorPokemon;
using WebApplicationEntrenadorPokemon.Entidades;
using WebApplicationEntrenadorPokemon.Servicios;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDBContext>(opciones => opciones.UseSqlServer("name=DefaultConnection"));
builder.Services.AddHttpContextAccessor();

builder.Services.AddTransient<IRepositorioUsuarios, RepositorioUsuarios>();

builder.Services.AddTransient<IRepositorioPokemon, RepositorioPokemon>();

builder.Services.AddTransient<IUserStore<Entrenador>, UsuarioStore>();

builder.Services.AddTransient<SignInManager<Entrenador>>();

builder.Services.AddIdentityCore<Entrenador>(opciones =>
{
    opciones.Password.RequireDigit = false;
    opciones.Password.RequireLowercase = false;
    opciones.Password.RequireUppercase = false;
    opciones.Password.RequireNonAlphanumeric = false;
    opciones.Password.RequiredLength = 4;
})
.AddSignInManager<SignInManager<Entrenador>>()
.AddUserStore<UsuarioStore>()
.AddDefaultTokenProviders();

builder.Services.AddAuthentication(opciones =>
{
    opciones.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
    opciones.DefaultSignOutScheme = IdentityConstants.ApplicationScheme;
    opciones.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
}).AddCookie(IdentityConstants.ApplicationScheme, options =>
{
    options.LoginPath = "/Usuarios/Login";
    options.AccessDeniedPath = "/Usuarios/Login";

    /*options.Events.OnSignedIn = async context =>
    {
        var usePrincipal = context.Principal;
        if (usePrincipal != null)
        {
            var userId = usePrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!string.IsNullOrEmpty(userId))
            {
                var userManger = context.HttpContext.RequestServices.GetRequiredService<UserManager<Entrenador>>();
                var user = await userManger.FindByIdAsync(userId);
                if (user != null)
                {
                    var claimIdentity = (ClaimsIdentity)usePrincipal.Identity;
                    claimIdentity.AddClaim(new Claim(ClaimTypes.GivenName, user.Nombre));
                    claimIdentity.AddClaim(new Claim(ClaimTypes.HomePhone, user.EntrenadorId));
                }

            }
        }
    };*/

});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Usuarios}/{action=Login}/{id?}");

app.Run();
