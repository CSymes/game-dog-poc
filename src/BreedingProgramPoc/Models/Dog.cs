#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
namespace BreedingProgramPoc.Models;

public record Dog
{
	public Phenotype<Colour> EyeColour { get; set; }
	public Phenotype<Colour> CoatColour { get; set; }
	public Phenotype<int> SnoutLength { get; set; }
	public Phenotype<bool> LongHair { get; set; }
}