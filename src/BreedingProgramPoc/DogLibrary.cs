namespace BreedingProgramPoc;

public static class DogLibrary
{
	public static Dog GoldenRetriever => new()
	{
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
}