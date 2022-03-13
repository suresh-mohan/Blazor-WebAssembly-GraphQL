using System.ComponentModel.DataAnnotations;

namespace BlazorWasmGraphQL.Shared.Dto
{
    public class UserLogin
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public UserLogin()
        {
            Username = string.Empty;
            Password = string.Empty;
        }
    }
}
