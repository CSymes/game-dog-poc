namespace BreedingProgramPoc.Models;

public record Phenotype<T>(Genotype<T> Genotype)
{
	private readonly Lazy<T> _lazyTrait = new(Genotype.GetResult);
	public T TraitValue => _lazyTrait.Value;
}