using Blazored.LocalStorage;
using CommunityEventPlanner.Shared.Service.Interface;
using CommunityEventPlanner.UI;
using CommunityEventPlanner.UI.Services.Implementation;
using CommunityEventPlanner.UI.Services.Interface;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using static System.Net.WebRequestMethods;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddAuthorizationCore();
builder.Services.AddBlazoredLocalStorage();

builder.Services.AddScoped<ICommunityEventService, CommunityEventService>();
builder.Services.AddHttpClient();
//To-Do move to config
builder.Services.AddHttpClient("AuthService", config =>
{
    config.BaseAddress = new Uri("https://localhost:7146/api/");
    config.Timeout = new TimeSpan(0, 0, 30);
    config.DefaultRequestHeaders.Clear();
});
//To-Do move to config
builder.Services.AddHttpClient("CommunityEventService", config =>
{
    config.BaseAddress = new Uri("https://localhost:7046/api/");
    config.Timeout = new TimeSpan(0, 0, 30);
    config.DefaultRequestHeaders.Clear();
});
builder.Services.AddScoped<AuthenticationStateProvider, CommunityEventAuthenticationStateProvider>();
builder.Services.AddScoped<IUserManagerService, UserManagerService>();
await builder.Build().RunAsync();
