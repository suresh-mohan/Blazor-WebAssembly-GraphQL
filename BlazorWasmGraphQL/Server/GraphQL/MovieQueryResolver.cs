using BlazorWasmGraphQL.Server.Interfaces;
using BlazorWasmGraphQL.Server.Models;

namespace BlazorWasmGraphQL.Server.GraphQL
{
    public class MovieQueryResolver
    {
        readonly IMovie _movieService;

        public MovieQueryResolver(IMovie movieService)
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
        [UseFiltering]
        public async Task<IQueryable<Movie>> GetMovieList()
        {
            List<Movie> availableMovies = await _movieService.GetAllMovies();
            return availableMovies.AsQueryable();
        }
    }
}
