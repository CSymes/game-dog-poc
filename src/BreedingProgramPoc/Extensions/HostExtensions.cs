using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BreedingProgramPoc.Extensions;

public static class HostExtensions
{
	public static IHostApplicationBuilder AddServices(this IHostApplicationBuilder builder)
	{
		builder.Services
			.AddSingleton<IDogMan, DogMan>()
			.AddSingleton<IDogLibrary, DogLibrary>()
			;

		builder.Services
			.AddTransient<Random>()
			;

		return builder;
	}
}