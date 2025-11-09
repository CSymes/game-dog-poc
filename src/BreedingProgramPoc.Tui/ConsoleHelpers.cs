using System.Text;

namespace BreedingProgramPoc.Tui;

public static class ConsoleHelpers
{
	public static void WriteBlockAtCurrentPos(string text)
	{
		var curPosLine = Console.CursorTop;
		var curPosCol = Console.CursorLeft;
		var lines = text.Split("\n");
		var mostFarCol = curPosCol;
		foreach (var line in lines)
		{
			Console.SetCursorPosition(curPosCol, curPosLine);
			Console.Write(line);
			curPosLine++;

			mostFarCol = Math.Max(mostFarCol, Console.CursorLeft);
		}

		Console.SetCursorPosition(mostFarCol, curPosLine - 1);
	}

	public static string WithBorder(this string text, bool withSpacing = true)
	{
		if (string.IsNullOrEmpty(text)) text = " ";

		var stringBuilder = new StringBuilder();
		var lines = text.TrimEnd('\n', '\r').Split("\n");
		var contentWidth = lines.MaxBy(s => s.TrimEnd('\n', '\r').Length)!.Length;

		stringBuilder.Append('+');
		stringBuilder.Append(new string('-', contentWidth + (withSpacing ? 2 : 0)));
		stringBuilder.Append('+');
		stringBuilder.Append('\n');
		if (withSpacing)
		{
			stringBuilder.Append('|');
			stringBuilder.Append(new string(' ', 2 + contentWidth));
			stringBuilder.Append('|');
			stringBuilder.Append('\n');
		}

		foreach (var l in lines)
		{
			var ll = l.TrimEnd('\n', '\r');
			stringBuilder.Append('|');
			if (withSpacing) stringBuilder.Append(' ');
			stringBuilder.Append(ll);
			stringBuilder.Append(new string(' ', contentWidth - ll.Length)); // pad to block width
			if (withSpacing) stringBuilder.Append(' ');
			stringBuilder.Append('|');
			stringBuilder.Append('\n');
		}

		if (withSpacing)
		{
			stringBuilder.Append('|');
			stringBuilder.Append(new string(' ', 2 + contentWidth));
			stringBuilder.Append('|');
			stringBuilder.Append('\n');
		}

		stringBuilder.Append('+');
		stringBuilder.Append(new string('-', contentWidth + (withSpacing ? 2 : 0)));
		stringBuilder.Append('+');
		stringBuilder.Append('\n');

		return stringBuilder.ToString();
	}

	public static T ChooseFrom<T>(Dictionary<string, T> options)
	{
		var keys = options.Keys.ToArray();
		for (var i = 0; i < keys.Length; i++) Console.WriteLine($"  {i}: {keys[i]}");
		while (true)
		{
			Console.Write("> ");
			var selection = Console.ReadKey().KeyChar.ToString();

			if (int.TryParse(selection, out var iSelected))
			{
				if (iSelected >= 0 && iSelected < options.Count)
				{
					Console.Write("\n");
					var key = keys[iSelected];
					return options[key];
				}
			}

			Console.WriteLine("No! Naughty!");
		}
	}
}