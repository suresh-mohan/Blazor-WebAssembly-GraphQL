using BlazorWasmGraphQL.Client.Shared;
using BlazorWasmGraphQL.Server.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace BlazorWasmGraphQL.Client.Pages
{
    public class WatchlistBase : ComponentBase
    {
        [Inject]
        AppStateContainer AppStateContainer { get; set; } = default!;

        [Inject]
        NavigationManager NavigationManager { get; set; } = default!;

        [CascadingParameter]
        Task<AuthenticationState> AuthenticationState { get; set; } = default!;

        protected List<Movie> watchlist = new();

        protected override async Task OnInitializedAsync()
        {
            AppStateContainer.OnAppStateChange += StateHasChanged;

            var authState = await AuthenticationState;

            if (authState.User.Identity is not null &&
                authState.User.Identity.IsAuthenticated)
            {
                GetUserWatchlist();
            }
            else
            {
                NavigationManager.NavigateTo($"login?returnUrl={Uri.EscapeDataString(NavigationManager.Uri)}");
            }
        }
        protected void WatchListClickHandler()
        {
            GetUserWatchlist();
        }

        void GetUserWatchlist()
        {
            watchlist = AppStateContainer.userWatchlist;
        }

        public void Dispose()
        {
            AppStateContainer.OnAppStateChange -= StateHasChanged;
        }
    }
}
