using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using BreedingProgramPoc.Extensions;

var builder = Host.CreateApplicationBuilder(args);
builder.AddServices();

var host = builder.Build();

// Start the background service
await host.RunAsync();
