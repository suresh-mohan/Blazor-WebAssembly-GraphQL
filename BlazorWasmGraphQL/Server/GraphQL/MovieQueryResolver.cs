using BlazorWasmGraphQL.Server.Interfaces;
using BlazorWasmGraphQL.Server.Models;

namespace BlazorWasmGraphQL.Server.GraphQL
{
    public class MovieQueryResolver
    {
        readonly IMovie _movieService;

        public MovieQueryResolver([Service] IMovie movieService)
        {
            _movieService = movieService;
        }

        [GraphQLDescription("Gets the list of genres.")]
        public async Task<List<Genre>> GetGenreList()
        {
            return await _movieService.GetGenre();
        }

        [GraphQLDescription("Gets the list of movies.")]
        [UseSorting]
        public async Task<IQueryable<Movie>> GetMovieList()
        {
            List<Movie> availableMovies = await _movieService.GetAllMovies();
            return availableMovies.AsQueryable();
        }

        [GraphQLDescription("Gets the movie data based on the movieId.")]
        public async Task<Movie> GetMovieById(int movieId)
        {
            return await _movieService.GetMovieData(movieId);
        }
    }
}
