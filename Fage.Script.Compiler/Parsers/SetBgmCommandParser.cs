using Fage.Script.Compiler.Syntax.Inlines;
using Markdig.Helpers;

namespace Fage.Script.Compiler.Parsers;

public class SetBgmCommandParser : FageCommandInlineParser<SetBgmCommandInline>
{
	public SetBgmCommandParser() : base("STBGM")
	{
		ArgsRequired = true;
	}

	protected override SetBgmCommandInline CreateInlineSyntax(StringSlice name, StringSlice args)
	{
		return new()
		{
			Args = args,
			Name = name,
			Bgm = args.ToString(),
		};
	}
}
