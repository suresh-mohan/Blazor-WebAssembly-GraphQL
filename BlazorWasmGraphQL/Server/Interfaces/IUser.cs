using BlazorWasmGraphQL.Server.Models;
using BlazorWasmGraphQL.Shared.Dto;

namespace BlazorWasmGraphQL.Server.Interfaces
{
    public interface IUser
    {
        UserLogin AuthenticateUser(UserLogin loginCredentials);
        UserLogin GetCurrentUser(string username);
        Task<bool> RegisterUser(UserMaster user);
        Task<bool> isUserExists(int userId);
    }
}
