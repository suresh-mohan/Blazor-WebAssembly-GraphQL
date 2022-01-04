using Microsoft.AspNetCore.Components;

namespace BlazorWasmGraphQL.Client.Pages
{
    public class MovieRatingBase : ComponentBase
    {
        [Parameter]
        public decimal? Rating { get; set; }
    }
}
