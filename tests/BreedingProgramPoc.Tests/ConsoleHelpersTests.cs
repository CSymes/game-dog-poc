using System.Diagnostics.CodeAnalysis;
using BreedingProgramPoc.Tui;

namespace BreedingProgramPoc.Tests;

[TestClass]
[SuppressMessage("ReSharper", "ConvertToConstant.Local")]
public class ConsoleHelpersTests
{
	[TestMethod]
	public void WithBorder_BasicTest()
	{
		// Arrange
		var input = "Hello World!";
		var expected = "+--------------+\n|              |\n| Hello World! |\n|              |\n+--------------+\n";

		// Act
		var output = input.WithBorder();

		// Assert
		Assert.AreEqual(expected, output);
	}

	[TestMethod]
	public void WithBorder_BasicTest_NoSpacing()
	{
		// Arrange
		var input = "Hello World!";
		var expected = "+------------+\n|Hello World!|\n+------------+\n";

		// Act
		var output = input.WithBorder(false);

		// Assert
		Assert.AreEqual(expected, output);
	}

	[TestMethod]
	public void WithBorder_Multiline()
	{
		// Arrange
		var input = "Hello World!\nI love trains... brrrrmmmmmm...";
		var expected =
			"+---------------------------------+\n|                                 |\n| Hello World!                    |\n| I love trains... brrrrmmmmmm... |\n|                                 |\n+---------------------------------+\n";

		// Act
		var output = input.WithBorder();

		// Assert
		Assert.AreEqual(expected, output);
	}
}