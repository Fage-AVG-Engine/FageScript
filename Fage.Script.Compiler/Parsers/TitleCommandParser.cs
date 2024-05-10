using Fage.Script.Compiler.Syntax.Inlines;
using Markdig.Helpers;

namespace Fage.Script.Compiler.Parsers;

public class TitleCommandParser : FageCommandInlineParser<TitleCommandInline>
{
	public TitleCommandParser() : base("TITLE")
	{
		ArgsRequired = false;
	}

	protected override TitleCommandInline CreateInlineSyntax(StringSlice name, StringSlice args)
	{
		return new() { Args = args, Name = name };
	}
}
