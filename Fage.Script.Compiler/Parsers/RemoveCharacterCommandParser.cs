using Fage.Script.Compiler.Syntax.Inlines;
using Markdig.Helpers;

namespace Fage.Script.Compiler.Parsers;

public class RemoveCharacterCommandParser : FageCommandInlineParser<RemoveCharacterCommandInline>
{
	public RemoveCharacterCommandParser() : base("RMCHR")
	{
		ArgsRequired = true;
	}

	protected override RemoveCharacterCommandInline CreateInlineSyntax(StringSlice name, StringSlice args)
	{
		return new()
		{
			Args = args,
			Name = name,
			CharacterId = args.ToString()
		};
	}
}
