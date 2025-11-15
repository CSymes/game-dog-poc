using BreedingProgramPoc.Interfaces;
using BreedingProgramPoc.Models;
using BreedingProgramPoc.Tui.Views;
using Microsoft.Extensions.DependencyInjection;
using Terminal.Gui.App;
using Terminal.Gui.ViewBase;
using Terminal.Gui.Views;

namespace BreedingProgramPoc.Tui.Windows;

public class BreedingWizard : Wizard
{
	private readonly WizardStep _dogPresentedStep;
	private readonly WizardStep _dog1Step;
	private readonly WizardStep _dog2Step;
	private readonly IDogMan _dogMan = DI.ServiceProvider.GetRequiredService<IDogMan>();
	private readonly TextField _nameField;
	private readonly Player _player;

	private readonly DogTabView _selector1;
	private readonly DogTabView _selector2;

	private Dog? _result;

	public BreedingWizard(Player player)
	{
		_player = player;
		Title = "Combobulating Puppies...";

		var dog1Step = new WizardStep();
		_dog1Step = dog1Step;
		_dog1Step.Title = "Select Dog #1...";
		_dog1Step.Add(_selector1 = new DogTabView(player)
		{
			Width = Dim.Fill(),
			Height = Dim.Fill(),
		});

		var dog2Step = new WizardStep();
		_dog2Step = dog2Step;
		_dog2Step.Title = "Select Dog #2...";
		_dog2Step.Add(_selector2 = new DogTabView(player)
		{
			Width = Dim.Fill(),
			Height = Dim.Fill(),
		});

		_dogPresentedStep = new WizardStep();
		_dogPresentedStep.Title = "Meet Your New Dog!";
		// rest of this step setup in the event below due to dynamic content

		var namingStep = new WizardStep();
		namingStep.Title = "Name Your New Dog!";
		_nameField = new TextField()
		{
			X = Pos.Center(),
			Y = Pos.Center(),
			Width = Dim.Percent(80),
			TextAlignment = Alignment.Center,
		};
		_nameField.TextChanged += (_, _) => { NextFinishButton.Enabled = !string.IsNullOrWhiteSpace(_nameField.Text); };
		namingStep.Add(_nameField);
		var nameLabel = new Label()
		{
			X = Pos.Center(),
			Y = Pos.Top(_nameField) - 1,
			Text = "Name:",
			TextAlignment = Alignment.Center,
		};
		namingStep.Add(nameLabel);


		AddStep(_dog1Step);
		AddStep(_dog2Step);
		AddStep(_dogPresentedStep);
		AddStep(namingStep);

		Finished += OnCompleteWizard;
		MovingNext += OnMovingNext_PerformBreedOnSubmit;
		MovingNext += OnMoving_HandleTabDisables;
		MovingBack += OnMoving_HandleTabDisables;
		Cancelled += OnCancelled;
	}

	private void OnCancelled(object? sender, WizardButtonEventArgs e)
	{
		e.Cancel = _result != null;
	}

	private void OnMoving_HandleTabDisables(object? sender, WizardButtonEventArgs e)
	{
		// ReSharper disable once UsePatternMatching
		var curDog1 = _selector1.SelectedTab?.Data as Dog;
		var curDog2 = _selector2.SelectedTab?.Data as Dog;

		if (curDog1 is not null)
		{
			_selector2.DisableDogs([curDog1]);
		}

		if (curDog2 is not null)
		{
			_selector1.DisableDogs([curDog2]);
		}
	}

	private void OnMovingNext_PerformBreedOnSubmit(object? sender, WizardButtonEventArgs evt)
	{
		if (GetNextStep() != _dogPresentedStep) return;

		if (_selector1.SelectedTab?.Data is not Dog dog1 ||
		    _selector2.SelectedTab?.Data is not Dog dog2)
		{
			evt.Cancel = true;
			return;
		}

		_dog1Step.Enabled = false;
		_dog2Step.Enabled = false;
		_result = _dogMan.Breed(dog1, dog2);

		_dogPresentedStep.Add(new Label
		{
			X = 0,
			Y = 0,
			Width = Dim.Percent(50),
			Height = Dim.Percent(50),
			Text = dog1.ToDisplayString(),
		});
		_dogPresentedStep.Add(new Label
		{
			X = Pos.Percent(50),
			Y = 0,
			Width = Dim.Percent(50),
			Height = Dim.Percent(50),
			Text = dog2.ToDisplayString(),
		});
		_dogPresentedStep.Add(new Line
		{
			X = Pos.Percent(50) - 1,
			Y = 0,
			Height = Dim.Percent(50),
			Orientation = Orientation.Vertical,
		});
		var line = new Line
		{
			X = 0,
			Y = Pos.Percent(50),
			Width = Dim.Percent(100),
		};
		_dogPresentedStep.Add(line);
		_dogPresentedStep.Add(new Label
		{
			X = Pos.Center(),
			Y = Pos.Bottom(line),
			Width = Dim.Auto(),
			Height = Dim.Fill(),
			Text = _result.ToDisplayString(false),
		});
	}

	private void OnCompleteWizard(object? sender, WizardButtonEventArgs? args)
	{
		if (_result is not null)
		{
			_result.Name = _nameField.Text.Trim();
			_player.AddDog(_result);
		}

		Application.RequestStop();
	}
}