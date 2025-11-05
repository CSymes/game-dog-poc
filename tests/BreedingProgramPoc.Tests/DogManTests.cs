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

	[TestMethod]
	public void TestBreeding()
	{
		// Arrange
		var dogA = new Dog
		{
			EyeColour = Phenotype<Colour>.PureBread(Colour.Blue, true),
			CoatColour = Phenotype<Colour>.PureBread(Colour.Brown, false),
			HairType = Phenotype<HairType>.PureBread(HairType.Short, true),
			HairLength = Phenotype<int>.PureBread(10, false),
			HasFringe = Phenotype<bool>.PureBread(false, true),
		};
		var dogB = new Dog
		{
			EyeColour = Phenotype<Colour>.PureBread(Colour.Green, false),
			CoatColour = Phenotype<Colour>.PureBread(Colour.Black, false),
			HairType = Phenotype<HairType>.PureBread(HairType.Curly, true),
			HairLength = Phenotype<int>.PureBread(3, false),
			HasFringe = Phenotype<bool>.PureBread(true, false),
		};

		// Act
		var result = _sut.Breed(dogA, dogB);

		// Assert
		var coatBlend = Colour.FromArgb(255, 82, 21, 21);
		Assert.AreEqual(coatBlend, result.CoatColour.TraitValue);
		Assert.AreEqual(HairType.Curly, result.HairType.TraitValue);
		Assert.AreEqual(Colour.Blue, result.EyeColour.TraitValue);
		Assert.AreEqual(6, result.HairLength.TraitValue);
	}
}