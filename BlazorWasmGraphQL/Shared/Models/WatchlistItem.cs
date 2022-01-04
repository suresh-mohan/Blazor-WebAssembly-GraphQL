namespace BlazorWasmGraphQL.Server.Models
{
    public partial class WatchlistItem
    {
        public int WatchlistItemId { get; set; }
        public string WatchlistId { get; set; } = null!;
        public int MovieId { get; set; }
    }
}
