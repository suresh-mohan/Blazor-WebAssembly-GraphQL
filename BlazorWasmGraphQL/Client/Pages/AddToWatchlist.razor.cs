using Syncfusion.Blazor.Notifications;
using BlazorWasmGraphQL.Client.GraphQLAPIClient;
using BlazorWasmGraphQL.Client.Shared;
using BlazorWasmGraphQL.Server.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace BlazorWasmGraphQL.Client.Pages
{
    public class AddToWatchlistBase : ComponentBase
    {

        [Inject]
        AppStateContainer AppStateContainer { get; set; } = default!;

        public static SfToast ToastObj;

        [Inject]
        MovieClient MovieClient { get; set; } = default!;

        [Parameter]
        public int MovieID { get; set; }

        [Parameter]
        public EventCallback WatchListClick { get; set; }

        [CascadingParameter]
        Task<AuthenticationState> AuthenticationState { get; set; } = default!;

        List<Movie> userWatchlist = new();
        protected bool toggle;
        protected string buttonText = string.Empty;

        public static List<ToastModel> Toast = new List<ToastModel>
        {
            new ToastModel{  Title = "SUCCESS", Content="Movie added to your Watchlist", CssClass="e-toast-success", Timeout=3000, ShowCloseButton=true},
            new ToastModel{ Title = "INFO", Content="Movie removed from your Watchlist", CssClass="e-toast-info", Timeout=3000, ShowCloseButton=true},
            new ToastModel{  Title = "SUCCESS", Content="Movie data is deleted successfully", CssClass="e-toast-success", Timeout=3000, ShowCloseButton=true}
        };

        int UserId { get; set; }

        protected override async Task OnInitializedAsync()
        {
            AppStateContainer.OnAppStateChange += StateHasChanged;

            var authState = await AuthenticationState;
            if (authState.User.Identity is not null &&
                authState.User.Identity.IsAuthenticated)
            {
                UserId = Convert.ToInt32(authState.User.FindFirst("userId").Value);
            }
        }

        protected override void OnParametersSet()
        {
            userWatchlist = AppStateContainer.userWatchlist;
            SetWatchlistStatus();
        }
        void SetWatchlistStatus()
        {
            var favouriteMovie = userWatchlist.Find(m => m.MovieId == MovieID);

            if (favouriteMovie != null)
            {
                toggle = true;
            }
            else
            {
                toggle = false;
            }

            SetButtonText();
        }
        void SetButtonText()
        {
            if (toggle)
            {
                buttonText = "Remove from Watchlist";
            }
            else
            {
                buttonText = "Add to Watchlist";
            }
        }

        protected async Task ToggleWatchList()
        {
            if (UserId > 0)
            {
                List<Movie> watchlist = new();
                toggle = !toggle;
                SetButtonText();

                var response = await MovieClient.ToggleWatchList.ExecuteAsync(UserId, MovieID);

                if (response.Data is not null)
                {
                    watchlist = response.Data.ToggleWatchlist.Select(x => new Movie
                    {
                        MovieId = x.MovieId,
                        Title = x.Title,
                        Duration = x.Duration,
                        Genre = x.Genre,
                        Language = x.Language,
                        PosterPath = x.PosterPath,
                        Rating = x.Rating,
                    }).ToList();
                }

                AppStateContainer.SetUserWatchlist(watchlist);

                if (toggle)
                {
                    ToastObj.Show(Toast[0]);
                }
                else
                {
                    ToastObj.Show(Toast[1]);
                }

                await WatchListClick.InvokeAsync();
            }
        }

        public void Dispose()
        {
            AppStateContainer.OnAppStateChange -= StateHasChanged;
        }
    }
}
