using BlazorWasmGraphQL.Client.GraphQLAPIClient;
using BlazorWasmGraphQL.Server.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorWasmGraphQL.Client.Pages
{
    public class HomeBase : ComponentBase
    {
        [Parameter]
        public string GenreName { get; set; } = default!;

        [Inject]
        MovieClient MovieClient { get; set; } = default!;

        protected List<Movie> lstMovie = new();
        protected List<Movie> filteredMovie = new();

        protected override async Task OnInitializedAsync()
        {
            MovieSortInput initialSort = new() { Title = SortEnumType.Asc };

            await GetMovieList(initialSort);
        }

        protected override void OnParametersSet()
        {
            FilterMovie();
        }

        async Task GetMovieList(MovieSortInput sortInput)
        {
            var results = await MovieClient.SortMovieList.ExecuteAsync(sortInput);

            if (results.Data is not null)
            {
                lstMovie = results.Data.MovieList.Select(x => new Movie
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

            filteredMovie = lstMovie;
        }

        void FilterMovie()
        {
            if (!string.IsNullOrEmpty(GenreName))
            {
                lstMovie = filteredMovie.Where(m => m.Genre == GenreName).ToList();
            }
            else
            {
                lstMovie = filteredMovie;
            }
        }

        protected async Task SortMovieData(ChangeEventArgs e)
        {
            switch (e.Value?.ToString())
            {
                case "title":
                    MovieSortInput titleSort = new() { Title = SortEnumType.Asc };
                    await GetMovieList(titleSort);
                    break;

                case "rating":
                    MovieSortInput ratingSort = new() { Rating = SortEnumType.Desc };
                    await GetMovieList(ratingSort);
                    break;

                case "duration":
                    MovieSortInput durationSort = new() { Duration = SortEnumType.Desc };
                    await GetMovieList(durationSort);
                    break;
            }
        }
    }
}
