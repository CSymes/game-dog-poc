using System.ComponentModel;
using BreedingProgramPoc.Models;
using Terminal.Gui.Views;

namespace BreedingProgramPoc.Tui.Views;

public class DogTabView : TabView
{
	private readonly Player _player;
	private Dog[] _disabledDogs = [];

	public bool AutoSwitch { get; set; }

	public DogTabView(Player player)
	{
		_player = player;
		_player.DogsChanged += PlayerOnDogsChanged;
		UpdateDogTabs();
	}

	private void PlayerOnDogsChanged(object? sender, CollectionChangeEventArgs e)
	{
		UpdateDogTabs();

		if (AutoSwitch && e.Action == CollectionChangeAction.Add)
		{
			SwitchTo(e.Element as Dog);
		}
	}

	public void DisableDogs(Dog[] dogs)
	{
		_disabledDogs = dogs;
		UpdateDogTabs();
	}

	private void UpdateDogTabs()
	{
		var currentSelection = SelectedTab?.Data as Dog;
		var existingTabs = Tabs.Select(t => t.Data as Dog).OfType<Dog>().ToHashSet();
		var playerDogs = _player.Dogs.ToHashSet();

		// remove all tabs - ensures that dog tabs are in the correct order
		Tabs.ToList().ForEach(RemoveTab);

		foreach (var dog in _player.Dogs)
		{
			var tab = new Tab
			{
				Data = dog,
				DisplayText = dog.Name,
				View = new Label
				{
					Text = dog.ToDisplayString(),
				},
			};
			AddTab(tab, false);
		}

		// remove tabs that no longer have dogs
		var badDogs = existingTabs.Except(playerDogs).ToHashSet().Union(_disabledDogs);
		var badTabs = Tabs.Where(t => t.Data is Dog dog && badDogs.Contains(dog)).ToList();
		foreach (var toRemove in badTabs)
		{
			RemoveTab(toRemove);
		}

		// reselect the originally selected tab, if it's still present
		SwitchTo(currentSelection);
	}

	protected override void Dispose(bool disposing)
	{
		base.Dispose(disposing);
		_player.DogsChanged -= PlayerOnDogsChanged;
	}

	private void SwitchTo(Dog? d)
	{
		if (d == null) return;

		var preferredTab = Tabs.FirstOrDefault(t => t.Data as Dog == d);
		if (preferredTab != null)
		{
			SelectedTab = preferredTab;
		}
	}
}