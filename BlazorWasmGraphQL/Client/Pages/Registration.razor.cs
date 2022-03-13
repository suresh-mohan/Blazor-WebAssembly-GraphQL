using BlazorWasmGraphQL.Client.GraphQLAPIClient;
using BlazorWasmGraphQL.Client.Shared;
using BlazorWasmGraphQL.Shared.Dto;
using Microsoft.AspNetCore.Components;

namespace BlazorWasmGraphQL.Client.Pages
{
    public class RegistrationBase : ComponentBase
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;

        [Inject]
        MovieClient MovieClient { get; set; } = default!;

        [Inject]
        ILogger<RegistrationBase> Logger { get; set; } = default!;

        protected UserRegistration registration = new();

        protected CustomValidator registerValidator;

        protected async Task RegisterUser()
        {
            registerValidator.ClearErrors();

            try
            {
                UserRegistrationInput registrationData = new()
                {
                    FirstName = registration.FirstName,
                    LastName = registration.LastName,
                    Username = registration.Username,
                    Password = registration.Password,
                    ConfirmPassword = registration.ConfirmPassword,
                    Gender = registration.Gender,
                };

                var response = await MovieClient.RegisterUser.ExecuteAsync(registrationData);

                if (response.Data is not null)
                {
                    RegistrationResponse RegistrationStatus = new()
                    {
                        IsRegistrationSuccess = response.Data.UserRegistration.IsRegistrationSuccess,
                        ErrorMessage = response.Data.UserRegistration.ErrorMessage
                    };

                    if (!RegistrationStatus.IsRegistrationSuccess)
                    {
                        registerValidator.DisplayErrors(nameof(registration.Username), RegistrationStatus.ErrorMessage);
                        throw new HttpRequestException($"User registration failed. Status Code: 403 Forbidden");
                    }
                    else
                    {
                        NavigationManager.NavigateTo("/login");
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message);
            }
        }
    }
}
