using BlazorWasmGraphQL.Server.Interfaces;
using BlazorWasmGraphQL.Server.Models;
using BlazorWasmGraphQL.Shared.Dto;
using BlazorWasmGraphQL.Shared.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BlazorWasmGraphQL.Server.GraphQL
{
    [ExtendObjectType(typeof(MovieMutation))]
    public class AuthMutationResolver
    {
        readonly IUser _userService;
        readonly IConfiguration _config;

        public AuthMutationResolver(IConfiguration config, IUser userService)
        {
            _config = config;
            _userService = userService;
        }

        [GraphQLDescription("Authenticate the user.")]
        public AuthResponse UserLogin(UserLogin userDetails)
        {
            UserLogin user = _userService.AuthenticateUser(userDetails);

            if (!string.IsNullOrEmpty(user.Username))
            {
                string tokenString = GenerateJWT(user);

                return new AuthResponse { Token = tokenString };
            }

            else
            {
                return new AuthResponse { ErrorMessage = "Username or Password is incorrect." };
            }
        }

        [GraphQLDescription("Register a new user.")]
        public async Task<RegistrationResponse> UserRegistration(UserRegistration registrationData)
        {
            UserMaster user = new()
            {
                FirstName = registrationData.FirstName,
                LastName = registrationData.LastName,
                Username = registrationData.Username,
                Password = registrationData.Password,
                Gender = registrationData.Gender,
                UserTypeName = UserRoles.User
            };

            bool userRegistrationStatus = await _userService.RegisterUser(user);

            if (userRegistrationStatus)
            {
                return new RegistrationResponse { IsRegistrationSuccess = true };
            }
            else
            {
                return new RegistrationResponse { IsRegistrationSuccess = false, ErrorMessage = "This User Name is not available." };
            }
        }

        string GenerateJWT(UserLogin userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            List<Claim> userClaims = new()
            {
                new Claim(ClaimTypes.Name, userInfo.Username),
                new Claim("userId", userInfo.UserId.ToString()),
                new Claim(ClaimTypes.Role, userInfo.UserTypeName),
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: userClaims,
                expires: DateTime.Now.AddHours(24),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
