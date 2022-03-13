using BlazorWasmGraphQL.Server.Models;
using BlazorWasmGraphQL.Shared.Dto;

namespace BlazorWasmGraphQL.Server.Interfaces
{
    public interface IUser
    {
        AuthenticatedUser AuthenticateUser(UserLogin loginCredentials);

        Task<bool> RegisterUser(UserMaster user);

        Task<bool> isUserExists(int userId);
    }
}
