using BlazorWasmGraphQL.Server.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorWasmGraphQL.Client.Pages
{
    public class MovieCardBase : ComponentBase
    {
        [Parameter]
        public Movie Movie { get; set; } = new();

        protected string imagePreview = string.Empty;

        protected override void OnParametersSet()
        {
            imagePreview = "/Poster/" + Movie.PosterPath;
        }
    }
}
