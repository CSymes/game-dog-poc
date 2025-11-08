namespace BreedingProgramPoc.Interfaces;

public interface IDogLibrary
{
	public IEnumerable<string> GetDogTypes();

	public Dog MakeDog(string type);
}