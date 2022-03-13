using Blazored.LocalStorage;
using BlazorWasmGraphQL.Client.GraphQLAPIClient;
using BlazorWasmGraphQL.Client.Shared;
using BlazorWasmGraphQL.Shared.Dto;
using Microsoft.AspNetCore.Components;

namespace BlazorWasmGraphQL.Client.Pages
{
    public class LoginBase : ComponentBase
    {
        [Parameter]
        [SupplyParameterFromQuery]
        public string returnUrl { get; set; } = default!;

        [Inject]
        NavigationManager NavigationManager { get; set; } = default!;

        [Inject]
        CustomAuthStateProvider CustomAuthStateProvider { get; set; } = default!;

        [Inject]
        MovieClient MovieClient { get; set; } = default!;

        [Inject]
        ILocalStorageService LocalStorageService { get; set; } = default!;

        [Inject]
        ILogger<LoginBase> Logger { get; set; } = default!;

        string _returnUrl = "/";
        protected UserLogin login = new();
        protected CustomValidator customValidator;

        protected override void OnInitialized()
        {
            if (returnUrl is not null)
            {
                _returnUrl = returnUrl;
            }
        }

        protected async Task AuthenticateUser()
        {
            customValidator.ClearErrors();

            try
            {
                UserLoginInput loginData = new()
                {
                    Password = login.Password,
                    Username = login.Username,
                };

                var response = await MovieClient.Login.ExecuteAsync(loginData);

                if (response.Data is not null)
                {
                    AuthResponse authResponse = new()
                    {
                        ErrorMessage = response.Data.UserLogin.ErrorMessage,
                        Token = response.Data.UserLogin.Token
                    };

                    if (authResponse.ErrorMessage is not null)
                    {
                        customValidator.DisplayErrors(nameof(login.Username), authResponse.ErrorMessage);
                        throw new HttpRequestException($"User validation failed. Status Code: 401 Unauthorized");
                    }
                    else
                    {
                        await LocalStorageService.SetItemAsync(AuthToken.TokenIdentifier, authResponse.Token);
                        CustomAuthStateProvider.NotifyAuthState();
                        NavigationManager.NavigateTo(_returnUrl);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message);
            }
        }
    }
}
