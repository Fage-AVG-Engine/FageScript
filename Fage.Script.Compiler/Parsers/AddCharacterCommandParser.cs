using Fage.Script.Compiler.Syntax.Inlines;
using Markdig.Helpers;
using System.Globalization;

namespace Fage.Script.Compiler.Parsers;

public class AddCharacterCommandParser : FageCommandInlineParser<AddCharacterCommandInline>
{
	public AddCharacterCommandParser() : base("ADCHR")
	{
		ArgsRequired = true;
	}

	protected override AddCharacterCommandInline CreateInlineSyntax(StringSlice name, StringSlice args)
	{
		var args1 = args;
		StringSlice id = ScanToSpace(ref args1);
		
		StringSlice motion = ScanToSpace(ref args1);
		StringSlice position = ScanToSpace(ref args1);

		return new()
		{
			Args = args,
			Name = name,
			CharacterId = id.ToString(),
			Motion = motion.IsEmpty ? null : motion.ToString(),
			Position = position.IsEmpty ? null : int.Parse(position.AsSpan(), NumberStyles.AllowLeadingSign),
		};
	}
}
