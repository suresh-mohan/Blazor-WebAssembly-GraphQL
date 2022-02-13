using BlazorWasmGraphQL.Server.Interfaces;
using BlazorWasmGraphQL.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorWasmGraphQL.Server.DataAccess
{
    public class MovieDataAccessLayer : IMovie
    {
        readonly MovieDBContext _dbContext;

        public MovieDataAccessLayer(IDbContextFactory<MovieDBContext> dbContext)
        {
            _dbContext = dbContext.CreateDbContext();
        }

        public async Task AddMovie(Movie movie)
        {
            try
            {
                await _dbContext.Movies.AddAsync(movie);
                await _dbContext.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<string> DeleteMovie(int movieId)
        {
            try
            {
                Movie? movie = await _dbContext.Movies.FindAsync(movieId);

                if (movie is not null)
                {
                    _dbContext.Movies.Remove(movie);
                    await _dbContext.SaveChangesAsync();
                    return movie.PosterPath;
                }

                return string.Empty;
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<Movie>> GetAllMovies()
        {
            return await _dbContext.Movies.AsNoTracking().ToListAsync();
        }
        async Task<Movie> GetMovieData(int movieId)
        {
            try
            {
                Movie? movie = new();

                movie = await _dbContext.Movies.FindAsync(movieId);

                if (movie is not null)
                {
                    _dbContext.Entry(movie).State = EntityState.Detached;
                }

                return movie;
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<Genre>> GetGenre()
        {
            return await _dbContext.Genres.AsNoTracking().ToListAsync();
        }

        public async Task<List<Movie>> GetMoviesAvailableInWatchlist(string watchlistID)
        {
            try
            {
                List<Movie> userWatchlist = new();
                List<WatchlistItem> watchlistItems =
                    _dbContext.WatchlistItems.Where(x => x.WatchlistId == watchlistID).ToList();

                foreach (WatchlistItem item in watchlistItems)
                {
                    Movie movie = await GetMovieData(item.MovieId);
                    if (movie?.MovieId > 0)
                    {
                        userWatchlist.Add(movie);
                    }
                }
                return userWatchlist;
            }
            catch
            {
                throw;
            }
        }

        public async Task UpdateMovie(Movie movie)
        {
            try
            {
                var result = await _dbContext.Movies.FirstOrDefaultAsync(e => e.MovieId == movie.MovieId);

                if (result is not null)
                {
                    result.Title = movie.Title;
                    result.Genre = movie.Genre;
                    result.Duration = movie.Duration;
                    result.PosterPath = movie.PosterPath;
                    result.Rating = movie.Rating;
                    result.Overview = movie.Overview;
                    result.Language = movie.Language;
                }

                await _dbContext.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }
    }
}
