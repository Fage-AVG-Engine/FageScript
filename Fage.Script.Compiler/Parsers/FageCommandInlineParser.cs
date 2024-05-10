using Fage.Script.Compiler.Syntax.Inlines;
using Markdig.Helpers;
using Markdig.Parsers;
using Markdig.Syntax;

namespace Fage.Script.Compiler.Parsers;

public abstract class FageCommandInlineParser<TSyntax> : InlineParser
	where TSyntax : FageCommandInline
{
	protected string CommandName { get; init; }

	protected bool ArgsRequired { get; init; }

	public FageCommandInlineParser(string commandName)
	{
		OpeningCharacters = ['$'];
		CommandName = commandName;
		ArgsRequired = false;
	}

	protected static StringSlice ScanToLineEnd(ref StringSlice slice)
	{
		if (slice.IsEmpty)
			return slice;

		int lineTerminatorIndex = slice.AsSpan().IndexOfAny(['\r', '\n']) - 1;

		if (lineTerminatorIndex == -2)
			lineTerminatorIndex = slice.Length - 1;

		int start = slice.Start;
		slice.Start += lineTerminatorIndex + 1;

		return new(slice.Text, start, start + lineTerminatorIndex, slice.NewLine);
	}

	protected static StringSlice ScanToSpace(ref StringSlice slice)
	{
		if (slice.IsEmpty)
			return slice;

		int spaceIndex = slice.AsSpan().IndexOf(' ') - 1;

		if (spaceIndex == -2)
			spaceIndex = slice.Length - 1;

		int start = slice.Start;
		slice.Start += spaceIndex + 1;

		return new(slice.Text, start, start + spaceIndex, slice.NewLine);
	}

	protected abstract TSyntax CreateInlineSyntax(StringSlice name, StringSlice args);

	public override bool Match(InlineProcessor processor, ref StringSlice slice)
	{
		ContainerBlock? ancestor = processor.Block!.Parent;

		while ((ancestor = ancestor?.Parent) != null)
			if (ancestor is QuoteBlock)
				return false;
		
		slice.SkipChar();

		if (slice.AsSpan().StartsWith(CommandName, StringComparison.OrdinalIgnoreCase))
		{
			StringSlice name = ScanToSpace(ref slice);

			char delimCandidate = slice.CurrentChar;

			if (ArgsRequired && !delimCandidate.IsSpace())
			{
				// 生成诊断
				ScanToLineEnd(ref slice);
				processor.Inline = new FageCommandInline() { Args = StringSlice.Empty, Name = name, ArgsMissing = true };
				return true;
			}

			StringSlice args;
			if (delimCandidate.IsSpace())
			{
				slice.TrimStart();
				args = ScanToLineEnd(ref slice);
			}
			else
			{
				args = StringSlice.Empty;
			}

			TSyntax syntax = CreateInlineSyntax(name, args);
			processor.Inline = syntax;

			return slice.SkipSpacesToEndOfLineOrEndOfDocument();
		}
		else
		{
			return false;
		}
	}
}
