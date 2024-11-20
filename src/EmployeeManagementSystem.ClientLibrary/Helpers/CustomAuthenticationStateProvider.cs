using EmployeeManagementSystem.BaseLibrary.Dtos.Client;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace EmployeeManagementSystem.ClientLibrary.Helpers
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly LocalStorageService _localStorageService;
        private readonly ClaimsPrincipal _claimsPrincipal;

        public CustomAuthenticationStateProvider(LocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
            _claimsPrincipal = new(new ClaimsIdentity());
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await _localStorageService.GetToken();
            if (string.IsNullOrEmpty(token)) return await Task.FromResult(new AuthenticationState(_claimsPrincipal));

            var deserializeToken = Serializations.DeserializeJsonString<UserSession>(token);
            if (deserializeToken == null) return await Task.FromResult(new AuthenticationState(_claimsPrincipal));

            var getUserClaims = DecryptToken(deserializeToken.Token);
            if (getUserClaims == null) return await Task.FromResult(new AuthenticationState(_claimsPrincipal));

            var claimsPrincipal = SetClaimsPrincipal(getUserClaims);
            return await Task.FromResult(new AuthenticationState(claimsPrincipal));
        }

        private static CustomUserClaims DecryptToken(string token)
        {
            if (string.IsNullOrEmpty(token))
                return new CustomUserClaims();

            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadJwtToken(token);
            var userId = jsonToken.Claims.FirstOrDefault(i => i.Type == ClaimTypes.NameIdentifier);
            var name = jsonToken.Claims.FirstOrDefault(i => i.Type == ClaimTypes.Name);
            var email = jsonToken.Claims.FirstOrDefault(i => i.Type == ClaimTypes.Email);
            var role = jsonToken.Claims.FirstOrDefault(i => i.Type == ClaimTypes.Role);
            return new CustomUserClaims(userId!.Value!, name!.Value!, email!.Value!, role!.Value!);
        }

        private static ClaimsPrincipal SetClaimsPrincipal(CustomUserClaims claims)
        {
            if (claims.Email == null) return new ClaimsPrincipal();
            return new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, claims.Id!),
                new(ClaimTypes.Name, claims.Name!),
                new(ClaimTypes.Email, claims.Email!),
                new(ClaimTypes.Role, claims.Role!)
            }, "JwtAuth"));
        }

        public async Task UpdateAuthenticationState(UserSession userSession)
        {
            var claimsPrincipal = new ClaimsPrincipal();
            if (userSession.Token != null && userSession.RefreshToken != null)
            {
                var serializeSession = Serializations.SerializeObj(userSession);
                await _localStorageService.SetToken(serializeSession);
                var getUserClaims = DecryptToken(userSession.Token);
                claimsPrincipal = SetClaimsPrincipal(getUserClaims!);
            }
            else
            {
                await _localStorageService.RemoveToken();
            }
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }
    }
}
