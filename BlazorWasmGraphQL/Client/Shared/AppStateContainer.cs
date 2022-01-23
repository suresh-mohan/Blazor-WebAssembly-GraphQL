using BlazorWasmGraphQL.Client.GraphQLAPIClient;
using BlazorWasmGraphQL.Server.Models;

namespace BlazorWasmGraphQL.Client.Shared
{
    public class AppStateContainer
    {
        private readonly MovieClient _movieClient;

        public AppStateContainer(MovieClient movieClient)
        {
            _movieClient = movieClient;
        }

        public List<Movie> userWatchlist = new();

        public List<Genre> AvailableGenre = new();

        public event Action OnAppStateChange = default!;

        public async Task GetAvailableGenre()
        {
            var results = await _movieClient.FetchGenreList.ExecuteAsync();

            if (results.Data is not null)
            {
                AvailableGenre = results.Data.GenreList.Select(x => new Genre
                {
                    GenreId = x.GenreId,
                    GenreName = x.GenreName,
                }).ToList();
            }
        }

        public async Task GetUserWatchlist(int userId)
        {
            List<Movie> currentUserWatchlist = new();

            if (userId > 0)
            {
                var response = await _movieClient.FetchWatchList.ExecuteAsync(userId);

                if (response.Data is not null)
                {
                    currentUserWatchlist = response.Data.Watchlist.Select(x => new Movie
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
            }

            SetUserWatchlist(currentUserWatchlist);
        }

        public void SetUserWatchlist(List<Movie> lstMovie)
        {
            userWatchlist = lstMovie;
            NotifyAppStateChanged();
        }

        private void NotifyAppStateChanged() => OnAppStateChange?.Invoke();
    }
}
