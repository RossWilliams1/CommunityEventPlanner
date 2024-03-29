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
builder.Services.AddScoped(sp =>
    new HttpClient());

builder.Services.AddScoped<ICommunityEventService, CommunityEventService>();

builder.Services.AddScoped<AuthenticationStateProvider, ComunityEventAuthenticationStateProvider>();
builder.Services.AddScoped<IUserManagerService, UserManagerService>();
await builder.Build().RunAsync();
