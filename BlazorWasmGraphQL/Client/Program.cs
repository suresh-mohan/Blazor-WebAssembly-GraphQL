using Blazored.LocalStorage;
using BlazorWasmGraphQL.Client;
using BlazorWasmGraphQL.Client.Shared;
using BlazorWasmGraphQL.Shared.Models;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Net.Http.Headers;
using Syncfusion.Blazor;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddOptions();
builder.Services.AddAuthorizationCore(config =>
{
    config.AddPolicy(UserRoles.Admin, Policies.AdminPolicy());
    config.AddPolicy(UserRoles.User, Policies.UserPolicy());
});

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddSyncfusionBlazor(options => { options.IgnoreScriptIsolation = true; });

string graphQLServerPath = builder.HostEnvironment.BaseAddress + "graphql";

builder.Services.AddMovieClient()
           .ConfigureHttpClient(client =>
           {
               client.BaseAddress = new Uri(graphQLServerPath);

               client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", AuthToken.TokenValue);
           }
);

// If you use the following line, the AuthenticationStateProvider will not send the updated state to components.
//And hence the nav bar will not be updated, without reloading. 
//builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddScoped<CustomAuthStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(provider => provider.GetRequiredService<CustomAuthStateProvider>());

builder.Services.AddScoped<AppStateContainer>();
builder.Services.AddBlazoredLocalStorage();

await builder.Build().RunAsync();
