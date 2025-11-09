namespace BreedingProgramPoc.Extensions;

public static class RandomExtensions
{
	public static bool NextBool(this Random rng) => rng.Next(2) == 1;

	public static TEnum NextEnum<TEnum>(this Random rng) where TEnum : struct, Enum
	{
		var poss = Enum.GetValues<TEnum>();
		var index = rng.Next(0, poss.Length);
		return poss[index];
	}
}