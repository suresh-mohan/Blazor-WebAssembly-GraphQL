namespace BlazorWasmGraphQL.Server.Interfaces
{
    public interface IWatchlist
    {
        Task ToggleWatchlistItem(int userId, int movieId);

        Task<string> GetWatchlistId(int userId);
    }
}
