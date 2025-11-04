using BreedingProgramPoc.Models;
using BreedingProgramPoc.Services;
using Colour = System.Drawing.Color;

namespace BreedingProgramPoc.Tests;

[TestClass]
public class DogManTests
{
	private Random _rng = null!;
	private DogMan _sut = null!;

	[TestInitialize]
	public void TestInitialize()
	{
		_rng = new Random();
		_sut = new DogMan(_rng);
	}

	#region Helpers

	private static Phenotype<T> MakePurebredPhenotype<T>(T trait, bool? dominant = null)
	{
		var a = new Allele<T>(trait, dominant ?? false);
		var b = new Allele<T>(trait, dominant ?? true);
		return new Phenotype<T>(new Genotype<T>(a, b));
	}

	#endregion

	[TestMethod]
	public void TestBreeding()
	{
		// Arrange
		var dogA = new Dog
		{
			EyeColour = MakePurebredPhenotype(Colour.Blue, true),
			CoatColour = MakePurebredPhenotype(Colour.Brown, false),
			LongHair = MakePurebredPhenotype(true, true),
			SnoutLength = MakePurebredPhenotype(5, false)
		};
		var dogB = new Dog
		{
			EyeColour = MakePurebredPhenotype(Colour.Green, false),
			CoatColour = MakePurebredPhenotype(Colour.Black, false),
			LongHair = MakePurebredPhenotype(false, false),
			SnoutLength = MakePurebredPhenotype(3, false)
		};

		// Act
		var result = _sut.Breed(dogA, dogB);

		// Assert
		var coatBlend = Colour.FromArgb(255, 82, 21, 21);
		Assert.AreEqual(coatBlend, result.CoatColour.TraitValue);
		Assert.IsTrue(result.LongHair.TraitValue);
		Assert.AreEqual(Colour.Blue, result.EyeColour.TraitValue);
		Assert.AreEqual(4, result.SnoutLength.TraitValue);
	}
}