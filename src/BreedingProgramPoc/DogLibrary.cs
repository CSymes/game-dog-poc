namespace BreedingProgramPoc;

public static class DogLibrary
{
	public static Dog GoldenRetriever => new()
	{
		BreedSize = BreedSize.Large,
		EyeColour = Colour.Brown,
		CoatColour = Colour.Gold,
		SnoutLength = 12,
		HairType = HairType.Straight,
		HairLength = 3,
		HasFringe = false,
		EarStyle = EarStyle.Flop,
	};

	public static Dog Poodle => new()
	{
		BreedSize = BreedSize.Medium,
		EyeColour = Colour.Brown,
		CoatColour = Colour.White,
		SnoutLength = 10,
		HairType = HairType.Curly,
		HairLength = 6,
		HasFringe = true,
		EarStyle = EarStyle.Flop,
	};

	public static Dog Labrador => new()
	{
		BreedSize = BreedSize.Large,
		EyeColour = Colour.Brown,
		CoatColour = Colour.Chocolate,
		SnoutLength = 12,
		HairType = HairType.Short,
		HairLength = 2,
		HasFringe = false,
		EarStyle = EarStyle.Flop,
	};

	public static Dog AussieShepherd => new()
	{
		BreedSize = BreedSize.Medium,
		EyeColour = Colour.Blue,
		CoatColour = Colour.FromArgb(255, 100, 100, 110), // Blue merle-ish gray
		SnoutLength = 11,
		HairType = HairType.Straight,
		HairLength = 5,
		HasFringe = true,
		EarStyle = EarStyle.Normal,
	};

	public static Dog BorderCollie => new()
	{
		BreedSize = BreedSize.Medium,
		EyeColour = Colour.Brown,
		CoatColour = Colour.Black,
		SnoutLength = 10,
		HairType = HairType.Straight,
		HairLength = 4,
		HasFringe = true,
		EarStyle = EarStyle.Normal,
	};

	public static Dog GermanShepherd => new()
	{
		BreedSize = BreedSize.Large,
		EyeColour = Colour.Brown,
		CoatColour = Colour.FromArgb(255, 139, 90, 43), // Tan/brown
		SnoutLength = 13,
		HairType = HairType.Straight,
		HairLength = 4,
		HasFringe = false,
		EarStyle = EarStyle.Perky,
	};
}