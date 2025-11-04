using System.Reflection;

namespace BreedingProgramPoc.Services;

public class DogMan(Random rng) : IDogMan
{
	public Dog Breed(Dog a, Dog b)
	{
		var d = new Dog();

		var breedGenMethod =
			GetType().GetMethod(nameof(BreedPhenotypes), BindingFlags.Instance | BindingFlags.NonPublic)!;

		foreach (var p in Dog.GetPhenotypeProperties())
		{
			var genA = p.GetValue(a);
			var genB = p.GetValue(b);

			var genericGenMethod = breedGenMethod.MakeGenericMethod(p.PropertyType.GenericTypeArguments[0]);
			var phenC = genericGenMethod.Invoke(this, [genA, genB]);

			p.SetValue(d, phenC);
		}

		return d;
	}

	private Phenotype<T> BreedPhenotypes<T>(Phenotype<T> phenA, Phenotype<T> phenB)
	{
		var genA = phenA.Genotype;
		var genB = phenB.Genotype;
		var alleleA = rng.NextBool() ? genA.A : genA.B;
		var alleleB = rng.NextBool() ? genB.A : genB.B;

		var gen = genA.ConstructNew(alleleA, alleleB);

		var phen = new Phenotype<T>(gen);
		return phen;
	}
}