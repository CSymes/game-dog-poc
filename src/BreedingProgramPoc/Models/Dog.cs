using System.Reflection;
using System.Text;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
namespace BreedingProgramPoc.Models;

public record Dog
{
	public Phenotype<Colour> EyeColour { get; set; }
	public Phenotype<Colour> CoatColour { get; set; }
	public Phenotype<int> SnoutLength { get; set; }
	public Phenotype<bool> LongHair { get; set; }

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
			.Where(p => p.PropertyType.GetGenericTypeDefinition() == typeof(Phenotype<>));

		return _phenotypeProps;
	}

	#endregion
}