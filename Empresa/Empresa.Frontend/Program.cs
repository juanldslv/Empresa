using CurrieTechnologies.Razor.SweetAlert2;
using Empresa.Frontend;
using Empresa.Frontend.Repositories;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5160/") });
builder.Services.AddScoped<IRepository, Repository>();
builder.Services.AddSweetAlert2();



await builder.Build().RunAsync();
