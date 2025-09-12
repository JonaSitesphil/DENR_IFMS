using System.Text.Json;
using System.Text.Json.Serialization;
using IFMS_V3;
using IFMS_V3.Services.Auth;
using IFMS_V3.Services.Burs;
using IFMS_V3.Services.Ors;
using IFMS_V3.Services.User;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor;
using MudBlazor.Services;
using IFMS_V3.Pdf; // 👈 your namespace
using PdfSharp.Fonts;
using IFMS_V3.Services.Norsa;
using IFMS_V3.Services.Signatories;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<AuthenticationStateProvider, CookieAuthenticationStateProvider>();
builder.Services.AddScoped<CsrfService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<OrsService>();
builder.Services.AddScoped<BursService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<NorsaService>();
builder.Services.AddScoped<SignatoryService>();



builder.Services.AddScoped(async sp =>
{
    var http = sp.GetRequiredService<HttpClient>();
    await CustomFontResolver.LoadFontAsync(http);  // preload font
    GlobalFontSettings.FontResolver = new CustomFontResolver();
    return http;
});


builder.Services.AddMudServices();  

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://192.168.1.167:7258")
    //BaseAddress = new Uri("https://10.0.0.22:7258")

});

builder.Services.AddSingleton(new JsonSerializerOptions
{
    Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) },
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
});

await builder.Build().RunAsync();
