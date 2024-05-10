using Fage.Script.Compiler.Syntax.Inlines;
using Markdig.Helpers;

namespace Fage.Script.Compiler.Parsers;

public class SfxCommandParser : FageCommandInlineParser<SfxCommandInline>
{
	public SfxCommandParser() : base("SFX")
	{
		ArgsRequired = true;
	}

	protected override SfxCommandInline CreateInlineSyntax(StringSlice name, StringSlice args)
	{
		return new()
		{
			Args = args,
			Name = name,
			SoundEffect = args.ToString()
		};
	}
}
