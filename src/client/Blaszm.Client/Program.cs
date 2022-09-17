using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Blaszm.Client;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

/*
if(_environment.IsDevelopment()) {
    builder.Services.AddCors(o => o.AddPolicy("BlazorCorsPolicy", b => b.SetIsOriginAllowed(s => new Uri(s).IsLoopback)));
  // or, explicitly allow client's address only
  //services.AddCors(o => o.AddPolicy("BlazorCorsPolicy", b => b.WithOrigins("http://localhost:6001")));
}
else {
  //...
}
*/

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

var app = builder.Build();
await app.RunAsync();
