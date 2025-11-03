namespace BreedingProgramPoc.Interfaces;

public interface IDogMan
{
	Dog MakeDog();
	Dog Breed(Dog a, Dog b);
}