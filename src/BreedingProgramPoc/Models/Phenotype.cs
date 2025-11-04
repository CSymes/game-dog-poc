namespace BreedingProgramPoc.Models;

public record Phenotype<T>(Genotype<T> Genotype)
{
	private readonly Lazy<T> _lazyTrait = new(Genotype.GetResult);
	public T TraitValue => _lazyTrait.Value;

	public override string ToString() => TraitValue! + $" [{Genotype}]";
	public static implicit operator Phenotype<T>(T input) => PureBread(input);

	public static Phenotype<T> PureBread(T trait, bool? dominant = null)
	{
		var a = new Allele<T>(trait, dominant ?? false);
		var b = new Allele<T>(trait, dominant ?? true);
		var loaf = new Phenotype<T>(new Genotype<T>(a, b));
		return loaf;
	}
}