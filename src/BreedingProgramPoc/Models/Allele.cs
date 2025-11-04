namespace BreedingProgramPoc.Models;

public record Allele<T>(T TraitValue, bool IsDominant)
{
	public override string ToString()
	{
		var ds = IsDominant ? "Dominant" : "Non-Dominant";
		return $"{TraitValue} ({ds})";
	}
}