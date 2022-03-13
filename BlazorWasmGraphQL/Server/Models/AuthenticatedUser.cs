namespace BlazorWasmGraphQL.Server.Models
{
    public class AuthenticatedUser
    {
        public int UserId { get; set; }

        public string Username { get; set; }

        public string UserTypeName { get; set; }

        public AuthenticatedUser()
        {
            Username = string.Empty;
            UserTypeName = string.Empty;
        }
    }
}
