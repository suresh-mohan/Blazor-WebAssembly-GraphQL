using BlazorWasmGraphQL.Client.Shared;
using BlazorWasmGraphQL.Server.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorWasmGraphQL.Client.Pages
{
    public class MovieGenreBase : ComponentBase
    {
        [Inject]
        NavigationManager NavigationManager { get; set; } = default!;

        [Inject]
        AppStateContainer AppStateContainer { get; set; } = default!;

        [Parameter]
        public string SelectedGenre { get; set; } = string.Empty;

        protected List<Genre> lstGenre = new();

        protected override void OnInitialized()
        {
            lstGenre = AppStateContainer.AvailableGenre;
        }

        protected void SelectGenre(string genreName)
        {
            if (string.IsNullOrEmpty(genreName))
            {
                NavigationManager.NavigateTo("/");
            }
            else
            {
                NavigationManager.NavigateTo("/category/" + genreName);
            }
        }
    }
}
