using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Blazored.LocalStorage;
using Fallaicious.App;
using Fallaicious.App.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddBlazoredLocalStorage();

var defaultTemplate = LoadEmbeddedTemplate();
builder.Services.AddSingleton(new TemplateService(defaultTemplate));
builder.Services.AddScoped<HistoryService>();

await builder.Build().RunAsync();

static string LoadEmbeddedTemplate()
{
    var assembly = typeof(Program).Assembly;
    var resourceName = "Fallaicious.App.Templates.DefaultTemplate.liquid";
    using var stream = assembly.GetManifestResourceStream(resourceName);
    if (stream is null) return string.Empty;
    using var reader = new StreamReader(stream);
    return reader.ReadToEnd();
}
