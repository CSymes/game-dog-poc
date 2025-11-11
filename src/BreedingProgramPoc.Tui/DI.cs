using Microsoft.Extensions.Hosting;

namespace BreedingProgramPoc.Tui;

public class DI : BackgroundService
{
	public DI(IServiceProvider serviceProvider)
	{
		ServiceProvider = serviceProvider;
	}

	public static IServiceProvider ServiceProvider { get; set; } = null!;

	protected override Task ExecuteAsync(CancellationToken stoppingToken) => Task.CompletedTask;
}