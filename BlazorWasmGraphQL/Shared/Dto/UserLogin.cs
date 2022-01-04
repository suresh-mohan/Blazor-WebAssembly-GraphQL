using System.ComponentModel.DataAnnotations;

namespace BlazorWasmGraphQL.Shared.Dto
{
    public class UserLogin
    {
        public int? UserId { get; set; }

        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        public string? UserTypeName { get; set; }
    }
}
