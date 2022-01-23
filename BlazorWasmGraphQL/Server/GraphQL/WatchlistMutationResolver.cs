using BlazorWasmGraphQL.Server.Interfaces;
using BlazorWasmGraphQL.Server.Models;
using HotChocolate.AspNetCore.Authorization;

namespace BlazorWasmGraphQL.Server.GraphQL
{
    [ExtendObjectType(typeof(MovieMutationResolver))]
    public class WatchlistMutationResolver
    {
        readonly IWatchlist _watchlistService;
        readonly IMovie _movieService;
        readonly IUser _userService;

        public WatchlistMutationResolver(IWatchlist watchlistService, IMovie movieService, IUser userService)
        {
            _watchlistService = watchlistService;
            _movieService = movieService;
            _userService = userService;
        }

        [Authorize]
        [GraphQLDescription("Get the user Watchlist.")]
        public async Task<List<Movie>> GetWatchlist(int userId)
        {
            return await GetUserWatchlist(userId);
        }

        [Authorize]
        [GraphQLDescription("Toggle Watchlist item.")]
        public async Task<List<Movie>> ToggleWatchlist(int userId, int movieId)
        {
            await _watchlistService.ToggleWatchlistItem(userId, movieId);
            return await GetUserWatchlist(userId);
        }

        async Task<List<Movie>> GetUserWatchlist(int userId)
        {
            bool user = await _userService.isUserExists(userId);

            if (user)
            {
                string watchlistid = await _watchlistService.GetWatchlistId(userId);
                return await _movieService.GetMoviesAvailableInWatchlist(watchlistid);
            }
            else
            {
                return new List<Movie>();
            }
        }
    }
}
