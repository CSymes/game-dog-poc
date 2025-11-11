using BreedingProgramPoc.Tui.Windows;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Terminal.Gui.App;

namespace BreedingProgramPoc.Tui;

public class TuiStartupService(ILogger<TuiStartupService> logger) : BackgroundService
{
	protected override Task ExecuteAsync(CancellationToken stoppingToken)
	{
		try
		{
			using var run = Application.Run<MainPage>();
			Application.Shutdown();
		}
		catch (Exception ex)
		{
			logger.LogError(ex, "{Error}", ex.Message);
		}

		return Task.CompletedTask;
	}

	/*
	private void ActionAddDog()
	{
		Console.WriteLine("Select which dog to add to inventory:");

		var dogTypes = library.GetDogTypes().ToList();
		var dogType = ConsoleHelpers.ChooseFrom(dogTypes.ToDictionary(d => d, d => d));
		var newDog = library.MakeDog(dogType);
		newDog.Name = dogType;
		player.Dogs.Add(newDog);
	}

	private void ActionBreed()
	{
		Console.WriteLine("Select first dog to breed:");
		var dog1 = ConsoleHelpers.ChooseFrom(player.Dogs.ToDictionary(d => d.Name, d => d));
		Console.WriteLine("Select second dog to breed:");
		var dog2 = ConsoleHelpers.ChooseFrom(player.Dogs.Except([dog1]).ToDictionary(d => d.Name, d => d));

		var newDog = dogMan.Breed(dog1, dog2);

		Console.Write($"Cute 🐕! What's your new ({newDog.Sex}) dog's name?\n>");
		var name = Console.ReadLine()?.Trim();
		if (!string.IsNullOrWhiteSpace(name)) newDog.Name = name;

		player.Dogs.Add(newDog);

		Console.WriteLine($"Welcome little {newDog.Name} to the world!");
	}
	*/
}