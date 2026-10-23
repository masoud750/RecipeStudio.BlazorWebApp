using Domain.Entities;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Components.Authorization;
using RecipeStudio.Infrastructure.Authentification;
using Microsoft.IdentityModel.Tokens;
using RecipeStudio.Infrastructure.Repositories;
using RecipeStudio.Application.Common.Interfaces;
using RecipeStudio.Application.Interfaces;
using RecipeStudio.Application.Services;
using RecipeStudio.Infrastructure.Logging;

using RecipeStudioBlazorApp.Server.Authentification;

using RecipeStudioBlazorApp.Server.Components;
using RecipeStudioBlazorApp.Server.Endpoints;
using Infrastructure.DependencyInjection;

using System.Text;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using RecipeStudio.Infrastructure;
using Microsoft.Fast.Components.FluentUI;
using MudBlazor;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// to maintain calean architecture we encapsulate dependency registration within 
// the infrastructure.
string connectionStr = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.RegisterDBService(connectionStr);

builder.Services.AddScoped<IRepository<Category>, CategoryRepository>();
builder.Services.AddScoped<ICategoryService,CategoryService>();
builder.Services.AddScoped<IRepository<User>, UserRepository>();
builder.Services.AddScoped<MudBlazor.IDialogService, MudBlazor.DialogService>();

//log

builder.Services.AddScoped<IAppLogger, NLogLogger>();

builder.Services.AddScoped<ITokenStorageService, TokenStorageService>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddScoped<IRegisterService, RegisterService>();
builder.Services.AddScoped<IUserRegisterService, UserRegisterService>();




// Controllers (API)
//builder.Services.AddControllers();
 



// HttpClient + Auth Handler
builder.Services.AddScoped<AuthHttpMessageHandler>();

builder.Services.AddScoped(sp =>
{
    var handler = sp.GetRequiredService<AuthHttpMessageHandler>();
    handler.InnerHandler = new HttpClientHandler();

    return new HttpClient(handler)
    {
        BaseAddress = new Uri(builder.Configuration["ApiBaseUrl"])
    };
});

// JWT Auth
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    var key = builder.Configuration["Jwt:Key"];
    var issuer = builder.Configuration["Jwt:Issuer"];
    var audience = builder.Configuration["Jwt:Audience"];

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = issuer,
        ValidAudience = audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key!))
    };
});

builder.Services.AddScoped<IAuthService, AuthService>();
// ------------------------------------------------------------
// Configure HttpClient with authentication handler.
// ------------------------------------------------------------
builder.Services.AddScoped(sp =>
{
    var handler = sp.GetRequiredService<AuthHttpMessageHandler>();
    handler.InnerHandler = new HttpClientHandler();
    var httpClient = new HttpClient(handler)
    {
        BaseAddress = new Uri(builder.Configuration["ApiBaseUrl"])
    };
    return httpClient;
});


var app = builder.Build();
app.ApplyMigration();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", 
                                createScopeForErrors: true);
    // The default HSTS (HSTS = HTTP Strict Transport Security) value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();
//app.MapControllers();
app.MapAuthEndpoint();// Add the auth endpoints
app.MapRegisterEndpoint();
app.Run();
