using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;

namespace BlazorWasmGraphQL.Client.Shared
{
    public class NavMenuBase : ComponentBase
    {
        [Inject]
        NavigationManager NavigationManager { get; set; } = default!;

        [Inject]
        CustomAuthStateProvider CustomAuthStateProvider { get; set; } = default!;

        [Inject]
        ILocalStorageService LocalStorageService { get; set; } = default!;

        protected async Task LogoutUser()
        {
            await LocalStorageService.RemoveItemAsync(AuthToken.TokenIdentifier);
            CustomAuthStateProvider.NotifyAuthState();
            NavigationManager.NavigateTo("/");
        }
    }
}
