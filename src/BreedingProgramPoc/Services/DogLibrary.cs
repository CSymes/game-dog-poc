using System.Reflection;

namespace BreedingProgramPoc.Services;

public class DogLibrary : IDogLibrary
{
	private static readonly Dictionary<string, Func<Dog>> DogInits;

	static DogLibrary()
	{
		DogInits = typeof(DogLibrary)
			.GetMethods()
			.Where(m =>
				m is { IsAbstract: false, IsStatic: true } &&
				m.ReturnParameter.ParameterType == typeof(Dog) &&
				m.GetParameters().Length == 0)
			.ToDictionary<MethodInfo, string, Func<Dog>>(m => m.Name, m => () => (Dog)m.Invoke(null, [])!);
	}

	public static Dog GoldenRetriever => new()
	{
		Name = "Fresh Golden Retriever",
		BreedSize = BreedSize.Large,
		LegProportions = 1.0,
		EyeColour = Colour.Brown,
		CoatColour = Colour.Gold,
		MuzzleType = MuzzleType.Snout,
		HairType = HairType.Straight,
		HairLength = 3,
		HasFringe = false,
		EarStyle = EarStyle.Flop,
		TailStyle = TailStyle.Bushy,
	};

	public static Dog Poodle => new()
	{
		Name = "Fresh Poodle",
		BreedSize = BreedSize.Medium,
		LegProportions = 1.05,
		EyeColour = Colour.Brown,
		CoatColour = Colour.White,
		MuzzleType = MuzzleType.Woofer,
		HairType = HairType.Curly,
		HairLength = 6,
		HasFringe = true,
		EarStyle = EarStyle.Flop,
		TailStyle = TailStyle.Pompom,
	};

	public static Dog Labrador => new()
	{
		Name = "Fresh Labrador",
		BreedSize = BreedSize.Large,
		LegProportions = 0.95,
		EyeColour = Colour.Brown,
		CoatColour = Colour.Chocolate,
		MuzzleType = MuzzleType.Snout,
		HairType = HairType.Short,
		HairLength = 2,
		HasFringe = false,
		EarStyle = EarStyle.Flop,
		TailStyle = TailStyle.Otter,
	};

	public static Dog AussieShepherd => new()
	{
		Name = "Fresh Aussie Shepherd",
		BreedSize = BreedSize.Medium,
		LegProportions = 1.0,
		EyeColour = Colour.Blue,
		CoatColour = Colour.FromArgb(255, 100, 100, 110), // Blue merle-ish gray
		MuzzleType = MuzzleType.Woofer,
		HairType = HairType.Straight,
		HairLength = 5,
		HasFringe = true,
		EarStyle = EarStyle.Normal,
		TailStyle = TailStyle.Bushy,
	};

	public static Dog BorderCollie => new()
	{
		Name = "Fresh Border Collie",
		BreedSize = BreedSize.Medium,
		LegProportions = 1.05,
		EyeColour = Colour.Brown,
		CoatColour = Colour.Black,
		MuzzleType = MuzzleType.Woofer,
		HairType = HairType.Straight,
		HairLength = 4,
		HasFringe = true,
		EarStyle = EarStyle.Shaggy,
		TailStyle = TailStyle.Bushy,
	};

	public static Dog GermanShepherd => new()
	{
		Name = "Fresh German Shepherd",
		BreedSize = BreedSize.Large,
		LegProportions = 1.1,
		EyeColour = Colour.Brown,
		CoatColour = Colour.FromArgb(255, 139, 90, 43), // Tan/brown
		MuzzleType = MuzzleType.Snout,
		HairType = HairType.Straight,
		HairLength = 4,
		HasFringe = false,
		EarStyle = EarStyle.Perky,
		TailStyle = TailStyle.Bushy,
	};

	public IEnumerable<string> GetDogTypes() => DogInits.Keys;

	public Dog MakeDog(string type) =>
		DogInits.TryGetValue(type, out var init)
			? init()
			: throw new ArgumentException($"Could not find dog type '{type}'");
}