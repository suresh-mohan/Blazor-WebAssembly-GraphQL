using BlazorWasmGraphQL.Client.GraphQLAPIClient;
using BlazorWasmGraphQL.Server.Models;
using BlazorWasmGraphQL.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace BlazorWasmGraphQL.Client.Pages
{
    public class ManageMoviesBase : ComponentBase
    {
        [Inject]
        MovieClient MovieClient { get; set; } = default!;

        [Inject]
        IAuthorizationService AuthService { get; set; } = default!;

        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;

        [CascadingParameter]
        public Task<AuthenticationState> AuthenticationState { get; set; } = default!;

        protected List<Movie>? lstMovie = new();
        protected Movie? movie = new();

        protected override async Task OnInitializedAsync()
        {
            var authState = await AuthenticationState;
            var CheckAdminPolicy = await AuthService.AuthorizeAsync(authState.User, Policies.AdminPolicy());

            if (CheckAdminPolicy.Succeeded)
            {
                await GetMovieList();
            }
            else
            {
                NavigationManager.NavigateTo($"login?returnUrl={Uri.EscapeDataString(NavigationManager.Uri)}");
            }
        }

        protected async Task GetMovieList()
        {
            var results = await MovieClient.FetchMovieList.ExecuteAsync();

            lstMovie = results?.Data?.MovieList.Select(x => new Movie
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

        protected void DeleteConfirm(int movieID)
        {
            movie = lstMovie?.FirstOrDefault(x => x.MovieId == movieID);
        }

        protected async Task DeleteMovie(int movieID)
        {
            var response = await MovieClient.DeleteMovieData.ExecuteAsync(movieID);

            if (response.Data is not null)
            {
                await GetMovieList();
                AddToWatchlistBase.ToastObj.Show(AddToWatchlistBase.Toast[2]);
            }
        }
    }
}
