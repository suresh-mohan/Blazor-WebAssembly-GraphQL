using System.ComponentModel.DataAnnotations;

namespace BlazorWasmGraphQL.Shared.Dto
{
    public class UserLogin
    {
        public int? UserId { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public string? UserTypeName { get; set; }

        public UserLogin()
        {
            Username = string.Empty;
            Password = string.Empty;
        }
    }
}
