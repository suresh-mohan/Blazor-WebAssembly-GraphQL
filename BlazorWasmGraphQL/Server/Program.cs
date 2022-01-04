using BlazorWasmGraphQL.Server.DataAccess;
using BlazorWasmGraphQL.Server.GraphQL;
using BlazorWasmGraphQL.Server.Interfaces;
using BlazorWasmGraphQL.Server.Models;
using BlazorWasmGraphQL.Shared.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
 .AddJwtBearer(options =>
 {
     options.RequireHttpsMetadata = false;
     options.SaveToken = true;
     options.TokenValidationParameters = new TokenValidationParameters
     {
         ValidateIssuer = true,
         ValidateAudience = true,
         ValidateLifetime = true,
         ValidateIssuerSigningKey = true,
         ValidIssuer = builder.Configuration["Jwt:Issuer"],
         ValidAudience = builder.Configuration["Jwt:Audience"],
         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"])),
     };
 });

builder.Services.AddAuthorization(config =>
{
    config.AddPolicy(UserRoles.Admin, Policies.AdminPolicy());
    config.AddPolicy(UserRoles.User, Policies.UserPolicy());
});

//builder.Services.AddCors(o => o.AddDefaultPolicy(b =>
//b.AllowAnyHeader()
//.AllowAnyMethod()
//.AllowAnyOrigin()));

builder.Services.AddPooledDbContextFactory<MovieDBContext>
    (options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Service injection here
builder.Services.AddScoped<IMovie, MovieDataAccessLayer>();
builder.Services.AddScoped<IUser, UserDataAccessLayer>();
builder.Services.AddScoped<IWatchlist, WatchlistDataAccessLayer>();

builder.Services.AddGraphQLServer()
    .AddAuthorization()
    //.AddDefaultTransactionScopeHandler()
    .AddQueryType<MovieQueryResolver>()
    .AddMutationType<MovieMutation>()
    .AddTypeExtension<AuthMutationResolver>()
    .AddTypeExtension<WatchlistMutationResolver>()
    .AddSorting();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

var FileProviderPath = app.Environment.ContentRootPath + "/Poster";
if (!Directory.Exists(FileProviderPath))
{
    Directory.CreateDirectory(FileProviderPath);
}

app.UseFileServer(new FileServerOptions
{
    FileProvider = new PhysicalFileProvider(FileProviderPath),
    RequestPath = "/Poster",
    EnableDirectoryBrowsing = true
});

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.UseEndpoints(endpoints =>
{
    endpoints.MapGraphQL();
});
app.MapFallbackToFile("index.html");

app.Run();
