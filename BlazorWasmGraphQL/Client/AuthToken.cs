namespace BlazorWasmGraphQL.Client
{
    public static class AuthToken
    {
        public static string TokenValue { get; set; } = string.Empty;

        public static string TokenIdentifier { get; set; } = "authToken";
    }
}
