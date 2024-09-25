using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using WebApplicationEntrenadorPokemon.Entidades;

namespace WebApplicationEntrenadorPokemon.Servicios
{
    public class UsuarioStore : IUserStore<Entrenador>, IUserEmailStore<Entrenador>, IUserPasswordStore<Entrenador>, IUserClaimStore<Entrenador>
    {
        private readonly IRepositorioUsuarios repositorioUsuarios;
        private readonly List<IdentityUserClaim<string>> userClaims = new List<IdentityUserClaim<string>>();
        private readonly List<Entrenador> users = new List<Entrenador>();


        public UsuarioStore(IRepositorioUsuarios repositorioUsuarios)
        {
            this.repositorioUsuarios = repositorioUsuarios;
        }

        public Task AddClaimsAsync(Entrenador user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
        {
            foreach (var claim in claims)
            {

                userClaims.Add(new IdentityUserClaim<string>
                {
                    UserId = Convert.ToString(user.Id),
                    ClaimType = claim.Type,
                    ClaimValue = claim.Value,
                });

            }

            return Task.CompletedTask;

        }

        public async Task<IdentityResult> CreateAsync(Entrenador user, CancellationToken cancellationToken)
        {
            await repositorioUsuarios.CrearEntrenador(user);
            return IdentityResult.Success;
        }

        public Task<IdentityResult> DeleteAsync(Entrenador user, CancellationToken cancellationToken)
        {
            return Task.FromResult(IdentityResult.Success);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task<Entrenador> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<Entrenador> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {

            return await repositorioUsuarios.BuscarPorId(Convert.ToInt32(userId));

        }

        //BUSCAR POR ID DE ENTRENADOR
        public async Task<Entrenador> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            var identrenador = normalizedUserName;
            return await repositorioUsuarios.BuscarEntrenadorPorId(identrenador.ToString());

        }

        public Task<IList<Claim>> GetClaimsAsync(Entrenador user, CancellationToken cancellationToken)
        {
            var claims = userClaims.Where(c => c.Id == user.Id)
                .Select(c => new Claim(c.ClaimType, c.ClaimValue)).ToList();

            return Task.FromResult<IList<Claim>>(claims);
        }

        public Task<string> GetEmailAsync(Entrenador user, CancellationToken cancellationToken)
        {

            return Task.FromResult(user.EntrenadorId);

        }

        public Task<bool> GetEmailConfirmedAsync(Entrenador user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetNormalizedEmailAsync(Entrenador user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }


        public Task<string> GetNormalizedUserNameAsync(Entrenador user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetPasswordHashAsync(Entrenador user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Password);
        }

        public Task<string> GetUserIdAsync(Entrenador user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Id.ToString());
        }

        public Task<string> GetUserNameAsync(Entrenador user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Nombre.ToString());
        }

        public Task<IList<Entrenador>> GetUsersForClaimAsync(Claim claim, CancellationToken cancellationToken)
        {
            var user = userClaims.Where(c => c.ClaimType == claim.Type && c.ClaimValue == claim.Value)
                .Select(c => users.FirstOrDefault(u => Convert.ToString(u.Id) == c.UserId))
                .Where(u => u != null).ToList();

            return Task.FromResult<IList<Entrenador>>(user);

        }

        public Task<bool> HasPasswordAsync(Entrenador user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task RemoveClaimsAsync(Entrenador user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task ReplaceClaimAsync(Entrenador user, Claim claim, Claim newClaim, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task SetEmailAsync(Entrenador user, string email, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task SetEmailConfirmedAsync(Entrenador user, bool confirmed, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;

        }

        public Task SetNormalizedEmailAsync(Entrenador user, string normalizedEmail, CancellationToken cancellationToken)
        {
            user.EntrenadorId = normalizedEmail;
            return Task.CompletedTask;
        }

        public Task SetNormalizedUserNameAsync(Entrenador user, string normalizedName, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task SetPasswordHashAsync(Entrenador user, string passwordHash, CancellationToken cancellationToken)
        {
            user.Password = passwordHash;
            return Task.CompletedTask;
        }

        public Task SetUserNameAsync(Entrenador user, string userName, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task<IdentityResult> UpdateAsync(Entrenador user, CancellationToken cancellationToken)
        {
            return Task.FromResult(IdentityResult.Success);
        }
    }
}
