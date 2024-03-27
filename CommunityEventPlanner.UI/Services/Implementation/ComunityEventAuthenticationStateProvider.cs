using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using CommunityEventPlanner.Contracts;
using System.Security.Claims;
namespace CommunityEventPlanner.UI.Services.Implementation
{
    public class ComunityEventAuthenticationStateProvider(ILocalStorageService localStorageService) : AuthenticationStateProvider
    {
        private readonly ClaimsPrincipal anonymous = new(new ClaimsIdentity());

        public async Task UpdateAuthTokenState(string? token)
        {
            ClaimsPrincipal claimsPrincipal = new();
            if (!string.IsNullOrWhiteSpace(token))
            {
                var userSession = TokenClaimManagement.GetClaims(token);
                claimsPrincipal = TokenClaimManagement.SetClaimPrincipal(userSession);
                await localStorageService.SetItemAsStringAsync("token", token);
            }
            else
            {
                claimsPrincipal = anonymous;
                await localStorageService.RemoveItemAsync("token");
            }

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }

        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                string stringToken = await localStorageService.GetItemAsStringAsync("token");

                if (string.IsNullOrWhiteSpace(stringToken))
                    return await Task.FromResult(new AuthenticationState(anonymous));

                var claims = TokenClaimManagement.GetClaims(stringToken);

                var claimsPrincipal = TokenClaimManagement.SetClaimPrincipal(claims);
                return await Task.FromResult(new AuthenticationState(claimsPrincipal));
            }
            catch
            {
                return await Task.FromResult(new AuthenticationState(anonymous));
            }
        }
    }
}