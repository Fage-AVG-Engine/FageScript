using Fage.Script.Compiler.Syntax.Inlines;
using Markdig.Helpers;

namespace Fage.Script.Compiler.Parsers;

public class VoiceCommandParser : FageCommandInlineParser<VoiceCommandInline>
{
	public VoiceCommandParser() : base("VOICE")
	{
		ArgsRequired = true;
	}

	protected override VoiceCommandInline CreateInlineSyntax(StringSlice name, StringSlice args)
	{
		return new()
		{
			Name = name,
			Args = args,
			Voice = args.ToString()
		};
	}
}
