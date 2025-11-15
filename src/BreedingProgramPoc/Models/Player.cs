using System.ComponentModel;

namespace BreedingProgramPoc.Models;

public class Player
{
	private readonly List<Dog> _dogs = [DogLibrary.AussieShepherd, DogLibrary.GermanShepherd, DogLibrary.Labrador];
	public IReadOnlyList<Dog> Dogs => _dogs;

	public void AddDog(Dog dog)
	{
		_dogs.Add(dog);
		DogsChanged?.Invoke(this, new CollectionChangeEventArgs(CollectionChangeAction.Add, dog));
	}

	public void RemoveDog(Dog dog)
	{
		_dogs.Remove(dog);
		DogsChanged?.Invoke(this, new CollectionChangeEventArgs(CollectionChangeAction.Remove, dog));
	}

	public event CollectionChangeEventHandler? DogsChanged;
}