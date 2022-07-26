using Blazored.LocalStorage;
using BlazorWasmGraphQL.Client.Shared;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Text.Json;

namespace BlazorWasmGraphQL.Client
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorage;
        private readonly AuthenticationState _anonymousUser;
        private readonly AppStateContainer _appStateContainer;

        public CustomAuthStateProvider(ILocalStorageService localStorage, AppStateContainer appStateContainer)
        {
            _localStorage = localStorage;
            _anonymousUser = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            _appStateContainer = appStateContainer;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            await _appStateContainer.GetAvailableGenre();
            AuthToken.TokenValue = await _localStorage.GetItemAsync<string>(AuthToken.TokenIdentifier);

            if (string.IsNullOrWhiteSpace(AuthToken.TokenValue))
            {
                return _anonymousUser;
            }

            List<Claim>? userClaims = ParseClaimsFromJwt(AuthToken.TokenValue).ToList();
            int UserId = Convert.ToInt32(userClaims.Find(claim => claim.Type == "userId")!.Value);

            if (UserId > 0)
            {
                await _appStateContainer.GetUserWatchlist(UserId);
            }
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(userClaims, "BlazorClientAuth")));
        }

        public void NotifyAuthState()
        {
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var claims = new List<Claim>();
            var payload = jwt.Split('.')[1];

            var jsonBytes = ParseBase64WithoutPadding(payload);

            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

            if (keyValuePairs is not null)
            {
                claims.AddRange(keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString())));
            }

            return claims;
        }

        static byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }
    }
}
