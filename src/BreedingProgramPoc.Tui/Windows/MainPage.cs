using BreedingProgramPoc.Interfaces;
using BreedingProgramPoc.Models;
using Microsoft.Extensions.DependencyInjection;
using Terminal.Gui.Input;
using Terminal.Gui.ViewBase;
using Terminal.Gui.Views;

namespace BreedingProgramPoc.Tui.Windows;

public sealed class MainPage : Window
{
	private readonly Dictionary<string, Action> _actions;

	private readonly OptionSelector _actionsSelector;
	private readonly IDogMan _dogMan = DI.ServiceProvider.GetRequiredService<IDogMan>();
	private readonly TabView _dogsView;
	private readonly IDogLibrary _library = DI.ServiceProvider.GetRequiredService<IDogLibrary>();
	private readonly Player _player = DI.ServiceProvider.GetRequiredService<Player>();

	public MainPage()
	{
		Title = "Dogs.";

		_actions = new Dictionary<string, Action>
		{
			["refresh"] = () => { },
			["add"] = ActionAddDog,
			["breed"] = ActionBreed,
		};

		_dogsView = new TabView
		{
			X = 0,
			Y = 0,
			Width = Dim.Fill(),
			Height = Dim.Percent(70),
		};

		var actionsPane = new FrameView
		{
			X = 0,
			Y = Pos.Bottom(_dogsView),
			Width = Dim.Fill(),
			Height = Dim.Fill(),
		};
		_actionsSelector = new OptionSelector
		{
			AssignHotKeysToCheckBoxes = true,
			Options = _actions.Keys.ToList(),
			CanFocus = true,
			SelectedItem = 0,
		};
		actionsPane.Add(_actionsSelector);

		UpdateDogTabs();

		Add(_dogsView);
		Add(actionsPane);

		AddCommand(Command.Accept, SelectAction);

		KeyBindings.Add(Key.CursorUp, Command.Up);
		AddCommand(Command.Up, UpAction);

		KeyBindings.Add(Key.CursorDown, Command.Down);
		AddCommand(Command.Down, DownAction);
	}

	private bool? SelectAction()
	{
		if (_actionsSelector.SelectedItem is null) return true;

		var actionName = _actionsSelector.Options?[_actionsSelector.SelectedItem.Value];

		if (string.IsNullOrEmpty(actionName) || !_actions.TryGetValue(actionName, out var action)) return true;

		action();

		return true;
	}

	private bool? UpAction()
	{
		if (_actionsSelector.SelectedItem == null)
		{
			_actionsSelector.SelectedItem = _actionsSelector.Options?.Count - 1;
		}
		else if (_actionsSelector.SelectedItem == 0) { }
		else
		{
			_actionsSelector.SelectedItem -= 1;
		}

		return true;
	}

	private bool? DownAction()
	{
		if (_actionsSelector.SelectedItem == null)
		{
			_actionsSelector.SelectedItem = 0;
		}
		else if (_actionsSelector.SelectedItem == _actionsSelector.Options?.Count - 1) { }
		else
		{
			_actionsSelector.SelectedItem += 1;
		}

		return true;
	}

	private void UpdateDogTabs()
	{
		var existingTabs = _dogsView.Tabs.Select(t => t.DisplayText).ToHashSet();
		var playerDogs = _player.Dogs.Select(d => d.Name).ToHashSet();

		foreach (var dog in _player.Dogs)
		{
			// skip dogs already existing as tabs
			if (existingTabs.Contains(dog.Name)) continue;

			var label = new Label
			{
				Text = dog.ToDisplayString(),
			};
			var tab = new Tab
			{
				DisplayText = dog.Name,
				View = label,
			};
			_dogsView.AddTab(tab, false);
		}

		// remove tabs that no longer have dogs
		var badDogs = existingTabs.Except(playerDogs);
		var badTabs = _dogsView.Tabs.Where(t => badDogs.Contains(t.DisplayText));
		foreach (var toRemove in badTabs)
		{
			_dogsView.RemoveTab(toRemove);
		}
	}

	private void ActionAddDog() { }

	private void ActionBreed() { }
}