using Microsoft.AspNetCore.Builder;
using System.IO.Compression;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.ResponseCompression;
using paynamart.com.Client;
using Blazored.LocalStorage;
using Toolbelt.Blazor.Extensions.DependencyInjection;
using paynamart.com.Client.Helper;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[] { "tr", "en" };
    options.DefaultRequestCulture = new RequestCulture("tr");
    options.AddSupportedCultures(supportedCultures);
    options.AddSupportedUICultures(supportedCultures);
});
builder.Services.Configure<GzipCompressionProviderOptions>
 (options => options.Level = CompressionLevel.Optimal);
builder.Services.AddResponseCompression(options =>
{
    options.Providers.Add<GzipCompressionProvider>();
});
builder.Services.AddI18nText();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<SessionState>();
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();

