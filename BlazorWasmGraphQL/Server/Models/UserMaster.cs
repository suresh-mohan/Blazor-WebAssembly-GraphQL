using System;
using System.Collections.Generic;

namespace BlazorWasmGraphQL.Server.Models
{
    public partial class UserMaster
    {
        public int UserId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public string UserTypeName { get; set; } = null!;
    }
}
