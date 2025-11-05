using System.Reflection;
using System.Text;

namespace BreedingProgramPoc.Models;

public record Dog
{
	public Sex Sex = Sex.Female;
	public int Age { get; set; } = 0;

	public Phenotype<BreedSize> BreedSize { get; init; } = Models.BreedSize.Small;
	public Phenotype<double> LegProportions { get; init; } = 1.0;
	public Phenotype<Colour> EyeColour { get; init; } = Colour.Gray; // TODO heterochromia
	public Phenotype<Colour> CoatColour { get; init; } = Colour.SandyBrown; // TODO patterns and multi-colours
	public Phenotype<MuzzleType> MuzzleType { get; init; } = Models.MuzzleType.Woofer;
	public Phenotype<HairType> HairType { get; init; } = Models.HairType.Short;
	public Phenotype<int> HairLength { get; init; } = 5;
	public Phenotype<bool> HasFringe { get; init; } = false;
	public Phenotype<EarStyle> EarStyle { get; init; } = Models.EarStyle.Normal;
	public Phenotype<TailStyle> TailStyle { get; init; } = Models.TailStyle.Otter;

	#region Display

	public override string ToString()
	{
		var sb = new StringBuilder();

		foreach (var prop in GetPhenotypeProperties())
		{
			var value = prop.GetValue(this);
			sb.AppendLine($"{prop.Name}: {value}");
		}

		return sb.ToString().TrimEnd();
	}

	public string ToDisplayString()
	{
		var sb = new StringBuilder();

		var nameLength = GetPhenotypeProperties().Select(p => p.Name.Length).Max();

		foreach (var prop in GetPhenotypeProperties())
		{
			dynamic value = prop.GetValue(this)!;
			sb.AppendLine($"{prop.Name.PadRight(nameLength)} = {value.TraitValue}");
		}

		return sb.ToString().TrimEnd();
	}

	private static IEnumerable<PropertyInfo>? _phenotypeProps;

	public static IEnumerable<PropertyInfo> GetPhenotypeProperties()
	{
		_phenotypeProps ??= typeof(Dog)
			.GetProperties()
			.Where(p =>
				p.PropertyType.IsGenericType &&
				p.PropertyType.GetGenericTypeDefinition() == typeof(Phenotype<>));

		return _phenotypeProps;
	}

	#endregion
}