namespace BreedingProgramPoc.Models;

public class Player
{
	public List<Dog> Dogs { get; } = [DogLibrary.AussieShepherd, DogLibrary.GermanShepherd, DogLibrary.Labrador];
}