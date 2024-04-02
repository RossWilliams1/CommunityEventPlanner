using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using CommunityEventPlanner.Shared;
using System.Security.Claims;
using CommunityEventPlanner.Shared.Service;
namespace CommunityEventPlanner.UI.Services.Implementation
{
    public class CommunityEventAuthenticationStateProvider(ILocalStorageService localStorageService) : AuthenticationStateProvider
    {
        private readonly ClaimsPrincipal anonymousUser = new(new ClaimsIdentity());

        public async Task UpdateAuthenticationState(string? token)
        {
            ClaimsPrincipal claimsPrincipal = new();
            if (!string.IsNullOrWhiteSpace(token))
            {
                var userSession = TokenClaimService.GetClaims(token);
                claimsPrincipal = TokenClaimService.SetClaimPrincipal(userSession);
                await localStorageService.SetItemAsStringAsync("token", token);
            }
            else
            {
                claimsPrincipal = anonymousUser;
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
                    return await Task.FromResult(new AuthenticationState(anonymousUser));

                var claims = TokenClaimService.GetClaims(stringToken);

                var claimsPrincipal = TokenClaimService.SetClaimPrincipal(claims);
                return await Task.FromResult(new AuthenticationState(claimsPrincipal));
            }
            catch
            {
                return await Task.FromResult(new AuthenticationState(anonymousUser));
            }
        }
    }
}