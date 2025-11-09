namespace BreedingProgramPoc.Models;

public record Genotype<T>(Allele<T> A, Allele<T> B)
{
	private static readonly Random _rng = new();

	public T GetResult()
	{
		// ReSharper disable once ConvertIfStatementToSwitchStatement
		if (A.IsDominant && !B.IsDominant) return A.TraitValue;
		if (A.IsDominant && B.IsDominant) return BlendAlleles();
		if (!A.IsDominant && B.IsDominant) return B.TraitValue;
		if (!A.IsDominant && !B.IsDominant) return BlendAlleles();

		throw new InvalidOperationException("Impossible state");
	}

	public Genotype<T> ConstructNew(Allele<T> a, Allele<T> b) => new(a, b);

	public override string ToString() => $"Genotype (A = {A}, B = {B})";

	private T BlendAlleles()
	{
		if (A.TraitValue == null || B.TraitValue == null) throw new InvalidOperationException();

		return A.TraitValue switch
		{
			short or int or long or float or double => AverageNumeric(
				A.TraitValue,
				B.TraitValue),
			Colour => (T)(object)AverageColour(
				(Colour)(object)A.TraitValue,
				(Colour)(object)B.TraitValue),
			Enum => (T)(object)ChooseEnum(
				(Enum)(object)A.TraitValue,
				(Enum)(object)B.TraitValue,
				A.IsDominant && B.IsDominant),
			bool => _rng.NextBool() ? A.TraitValue : B.TraitValue,
			_ => throw new ArgumentException(nameof(T), "Unblendable trait type T=" + typeof(T).Name),
		};
	}

	private static T AverageNumeric(T a, T b) => (T)(object)(((dynamic)a! + (dynamic)b!) / 2);

	private static Colour AverageColour(Colour a, Colour b) =>
		Colour.FromArgb(
			(a.A + b.A) / 2,
			(a.R + b.R) / 2,
			(a.G + b.G) / 2,
			(a.B + b.B) / 2
		);

	private static Enum ChooseEnum(Enum a, Enum b, bool chooseUpper)
	{
		var alt = a.CompareTo(b) < 0;
		if (chooseUpper) return alt ? b : a;
		return alt ? a : b;
	}
}