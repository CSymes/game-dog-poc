namespace BreedingProgramPoc.Genotypes;

public record DominanceGenotype<T>(Allele<T> A, Allele<T> B) : Genotype<T>(A, B)
{
	public override T GetResult()
	{
		// ReSharper disable once ConvertIfStatementToSwitchStatement
		if (A.IsDominant && !B.IsDominant) return A.TraitValue;
		if (A.IsDominant && B.IsDominant) return BlendAlleles();
		if (!A.IsDominant && B.IsDominant) return B.TraitValue;
		if (!A.IsDominant && !B.IsDominant) return BlendAlleles();

		throw new InvalidOperationException("Impossible state");
	}

	public override Genotype<T> ConstructNew(Allele<T> A, Allele<T> B)
	{
		return new DominanceGenotype<T>(A, B);
	}

	private T BlendAlleles()
	{
		return A.TraitValue switch
		{
			short or int or long or float or double => AverageNumeric(A.TraitValue, B.TraitValue),
			Colour c => (T)(object)AverageColour((Colour)(object)A.TraitValue!, (Colour)(object)B.TraitValue!),
			_ => throw new ArgumentException(nameof(T), "Unblendable trait type T=" + typeof(T).Name)
		};
	}

	private static T AverageNumeric(T a, T b)
	{
		return (T)(object)(((dynamic)a! + (dynamic)b!) / 2);
	}

	private static Colour AverageColour(Colour a, Colour b)
	{
		return Colour.FromArgb(
			(a.A + b.A) / 2,
			(a.R + b.R) / 2,
			(a.G + b.G) / 2,
			(a.B + b.B) / 2
		);
	}
}