using BreedingProgramPoc.Models;
using BreedingProgramPoc.Services;
using BreedingProgramPoc.Tui.Views;
using Terminal.Gui.App;
using Terminal.Gui.Input;
using Terminal.Gui.ViewBase;
using Terminal.Gui.Views;

namespace BreedingProgramPoc.Tui.Windows;

public sealed class MainPage : Window
{
	private readonly Dictionary<string, Action> _actions;

	private readonly OptionSelector _actionsSelector;
	private readonly Player _player = new();

	public MainPage()
	{
		Title = "Dogs.";

		_actions = new Dictionary<string, Action>
		{
			["refresh"] = () => { },
			["add"] = ActionAddDog,
			["breed"] = ActionBreed,
		};

		var dogsView = new DogTabView(_player)
		{
			X = 0,
			Y = 0,
			Width = Dim.Fill(),
			Height = Dim.Percent(70),
			AutoSwitch = true,
		};

		var actionsPane = new FrameView
		{
			X = 0,
			Y = Pos.Bottom(dogsView),
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

		Add(dogsView);
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

	private void ActionAddDog()
	{
		_player.AddDog(DogLibrary.Poodle);
	}

	private void ActionBreed()
	{
		var b = new BreedingWizard(_player);
		Application.Run(b);
	}
}