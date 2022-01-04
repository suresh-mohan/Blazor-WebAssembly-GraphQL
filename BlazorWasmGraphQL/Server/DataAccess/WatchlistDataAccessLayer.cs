using BlazorWasmGraphQL.Server.Interfaces;
using BlazorWasmGraphQL.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorWasmGraphQL.Server.DataAccess
{
    public class WatchlistDataAccessLayer : IWatchlist
    {
        readonly MovieDBContext _dbContext;

        public WatchlistDataAccessLayer(IDbContextFactory<MovieDBContext> dbContext)
        {
            _dbContext = dbContext.CreateDbContext();
        }

        string CreateWatchlist(int userId)
        {
            try
            {
                Watchlist watchlist = new()
                {
                    WatchlistId = Guid.NewGuid().ToString(),
                    UserId = userId,
                    DateCreated = DateTime.Now.Date
                };

                _dbContext.Watchlists.Add(watchlist);
                _dbContext.SaveChanges();

                return watchlist.WatchlistId;
            }
            catch
            {
                throw;
            }
        }

        public async Task<string> GetWatchlistId(int userId)
        {
            try
            {
                Watchlist? watchlist = await _dbContext.Watchlists.FirstOrDefaultAsync(x => x.UserId == userId);

                if (watchlist is not null)
                {
                    return watchlist.WatchlistId;
                }
                else
                {
                    return CreateWatchlist(userId);
                }

            }
            catch
            {
                throw;
            }
        }

        public async Task ToggleWatchlistItem(int userId, int movieId)
        {
            string watchlistId = await GetWatchlistId(userId);

            WatchlistItem? existingWatchlistItem = await _dbContext.WatchlistItems
                .FirstOrDefaultAsync(x => x.MovieId == movieId && x.WatchlistId == watchlistId);

            if (existingWatchlistItem is not null)
            {
                _dbContext.WatchlistItems.Remove(existingWatchlistItem);
            }
            else
            {
                WatchlistItem watchlistItem = new()
                {
                    WatchlistId = watchlistId,
                    MovieId = movieId,
                };

                _dbContext.WatchlistItems.Add(watchlistItem);
            }
            await _dbContext.SaveChangesAsync();
        }
    }
}
