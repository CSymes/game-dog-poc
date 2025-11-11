using BreedingProgramPoc.Extensions;
using BreedingProgramPoc.Tui;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

var builder = Host.CreateApplicationBuilder(args);
builder.AddServices();
builder.Services.AddHostedService<DI>();
builder.Services.AddHostedService<TuiStartupService>();
// builder.Logging.ClearProviders();
builder.Logging.SetMinimumLevel(LogLevel.Warning);

var host = builder.Build();

await host.RunAsync();