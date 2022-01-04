using BlazorWasmGraphQL.Server.Models;

namespace BlazorWasmGraphQL.Server.Interfaces
{
    public interface IMovie
    {
        Task AddMovie(Movie movie);

        Task<List<Genre>> GetGenre();

        Task<List<Movie>> GetAllMovies();

        Task<Movie> GetMovieData(int movieId);

        Task UpdateMovie(Movie movie);

        Task<string> DeleteMovie(int movieId);

        Task<List<Movie>> GetMoviesAvailableInWatchlist(string watchlistID);
    }
}
