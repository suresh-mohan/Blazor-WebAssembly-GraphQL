using BlazorWasmGraphQL.Client.GraphQLAPIClient;
using BlazorWasmGraphQL.Client.Shared;
using BlazorWasmGraphQL.Server.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace BlazorWasmGraphQL.Client.Pages
{
    public class AddEditMovieBase : ComponentBase
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;

        [Inject]
        MovieClient MovieClient { get; set; } = default!;

        [Inject]
        AppStateContainer AppStateContainer { get; set; } = default!;

        [Parameter]
        public int MovieID { get; set; }

        protected string Title = "Add";
        public Movie movie = new();
        protected List<Genre>? lstGenre = new();
        protected string? imagePreview;
        const int MaxFileSize = 10 * 1024 * 1024; // 10 MB
        const string DefaultStatus = "Maximum size allowed for the image is 10 MB";
        protected string status = DefaultStatus;

        protected override void OnInitialized()
        {
            lstGenre = AppStateContainer.AvailableGenre;
        }

        protected override async Task OnParametersSetAsync()
        {
            if (MovieID != 0)
            {
                Title = "Edit";

                var response = await MovieClient.FetchMovieDetails.ExecuteAsync(MovieID);
                var movieData = response?.Data?.MovieById;
                if (movieData is not null)
                {
                    movie.MovieId = movieData.MovieId;
                    movie.Title = movieData.Title;
                    movie.Genre = movieData.Genre;
                    movie.Duration = movieData.Duration;
                    movie.PosterPath = movieData.PosterPath;
                    movie.Rating = movieData.Rating;
                    movie.Overview = movieData.Overview;
                    movie.Language = movieData.Language;

                    imagePreview = "/Poster/" + movie.PosterPath;
                }
            }
        }

        protected async Task SaveMovie()
        {
            MovieInput movieData = new()
            {
                MovieId = movie.MovieId,
                Title = movie.Title,
                Overview = movie.Overview,
                Duration = movie.Duration,
                Rating = movie.Rating,
                Genre = movie.Genre,
                Language = movie.Language,
                PosterPath = movie.PosterPath,
            };

            if (movieData.MovieId != 0)
            {
                await MovieClient.EditMovieData.ExecuteAsync(movieData);
            }
            else
            {
                await MovieClient.AddMovieData.ExecuteAsync(movieData);
            }

            NavigateToAdminPanel();
        }

        protected void NavigateToAdminPanel()
        {
            NavigationManager?.NavigateTo("/admin/movies");
        }

        protected async Task ViewImage(InputFileChangeEventArgs e)
        {
            if (e.File.Size > MaxFileSize)
            {
                status = $"The file size is {e.File.Size} bytes, this is more than the allowed limit of {MaxFileSize} bytes.";
                return;
            }
            else if (!e.File.ContentType.Contains("image"))
            {
                status = "Please upload a valid image file";
                return;
            }
            else
            {
                using var reader = new StreamReader(e.File.OpenReadStream(MaxFileSize));

                var format = "image/jpeg";
                var imageFile = await e.File.RequestImageFileAsync(format, 640, 480);

                using var fileStream = imageFile.OpenReadStream(MaxFileSize);
                using var memoryStream = new MemoryStream();
                await fileStream.CopyToAsync(memoryStream);

                imagePreview = $"data:{format};base64,{Convert.ToBase64String(memoryStream.ToArray())}";
                movie.PosterPath = Convert.ToBase64String(memoryStream.ToArray());

                status = DefaultStatus;
            }
        }
    }
}
