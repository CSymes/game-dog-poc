using BreedingProgramPoc.Interfaces;
using BreedingProgramPoc.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BreedingProgramPoc.Tui;

public class Tui(
	ILogger<Tui> logger,
	IHostApplicationLifetime lifetime,
	IDogLibrary library,
	IDogMan dogMan,
	Player player)
	: IHostedService
{
	private Dictionary<string, Action>? _actionsField;

	private bool _running;

	private Dictionary<string, Action> Actions
	{
		get
		{
			_actionsField ??= LoadActions();
			return _actionsField;
		}
	}

	private static int Width => Console.WindowWidth;
	private static int Height => Console.WindowHeight;

	public Task StartAsync(CancellationToken cancellationToken)
	{
		_running = true;
		Task.Run(() =>
		{
			try
			{
				Loop();
			}
			catch (Exception ex)
			{
				logger.LogError(ex, "{Error}", ex.Message);
			}
		}, cancellationToken);
		return Task.CompletedTask;
	}

	public Task StopAsync(CancellationToken cancellationToken)
	{
		_running = false;
		return Task.CompletedTask;
	}

	private Dictionary<string, Action> LoadActions()
	{
		var actions = new Dictionary<string, Action>
		{
			["refresh"] = () => { },
			["add"] = ActionAddDog,
			["breed"] = ActionBreed,
		};
		return actions;
	}

	private void Loop()
	{
		while (_running)
		{
			Console.Clear();
			Console.SetCursorPosition(1, 1);

			WriteDogs();

			Console.SetCursorPosition(1, Console.CursorTop + 2);
			Console.WriteLine(new string('-', Console.WindowWidth - 2));
			Console.SetCursorPosition(1, Console.CursorTop + 2);

			Console.WriteLine("Choose an action:");
			var iKeys = Actions.Keys.ToList();
			for (var i = 0; i < iKeys.Count; i++)
			{
				var k = iKeys[i];
				Console.WriteLine($"  {i}: {k}");
			}

			Action? selectedAction = null;
			while (_running)
			{
				Console.Write("> ");
				var read = Console.ReadKey();
				var selection = read.KeyChar.ToString();

				if (int.TryParse(selection, out var iSelected))
				{
					if (iSelected >= 0 && iSelected < iKeys.Count)
					{
						selectedAction = Actions[iKeys[iSelected]];
						break;
					}
				}
				else if (read.Key == ConsoleKey.Escape)
				{
					lifetime.StopApplication();
					return;
				}
				else if (read.Key == ConsoleKey.F5)
				{
					selectedAction = Actions["refresh"];
					break;
				}

				Console.WriteLine("No! Naughty!");
			}

			Console.WriteLine("");
			selectedAction?.Invoke();
		}
	}

	private void WriteDogs()
	{
		var initialX = Console.CursorLeft;
		var curRowTop = Console.CursorTop;
		var curRowBottom = Console.CursorTop;
		var dogsSinceCarriageReturn = 0;
		foreach (var dog in player.Dogs)
		{
			var s = dog.ToDisplayString();
			var boxed = s.WithBorder();

			var additionalWidth = boxed.IndexOf('\n');

			if (Console.CursorLeft + additionalWidth > Width && dogsSinceCarriageReturn > 0)
			{
				Console.SetCursorPosition(initialX, curRowBottom + 1);
				curRowTop = Console.CursorTop;
				dogsSinceCarriageReturn = 0;
			}

			ConsoleHelpers.WriteBlockAtCurrentPos(boxed);

			curRowBottom = Math.Max(curRowBottom, Console.CursorTop);
			dogsSinceCarriageReturn++;
			Console.SetCursorPosition(Console.CursorLeft + 1, curRowTop);
		}

		Console.SetCursorPosition(Console.CursorLeft, curRowBottom);
	}

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
}