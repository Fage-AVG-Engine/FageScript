using Fage.Script.Compiler.Syntax.Inlines;
using Markdig.Helpers;

namespace Fage.Script.Compiler.Parsers;

public class BackgroundCommandParser : FageCommandInlineParser<BackgroundCommandInline>
{
	public BackgroundCommandParser() : this("SETBG")
	{

	}

	public BackgroundCommandParser(string aliasCommandName) : base(aliasCommandName)
	{
		ArgsRequired = true;
	}

	protected override BackgroundCommandInline CreateInlineSyntax(StringSlice name, StringSlice args)
	{
		return new()
		{
			Name = name,
			Args = args,
			Background = args.ToString()
		};
	}
}
