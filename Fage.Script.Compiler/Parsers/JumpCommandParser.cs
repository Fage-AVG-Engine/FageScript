using Fage.Script.Compiler.Syntax.Inlines;
using Markdig.Helpers;

namespace Fage.Script.Compiler.Parsers;

public class JumpCommandParser : FageCommandInlineParser<JumpCommandInline>
{
	public JumpCommandParser() : base("JUMP")
	{
		ArgsRequired = true;
	}

	protected override JumpCommandInline CreateInlineSyntax(StringSlice name, StringSlice args)
	{
		var args1 = args;
		StringSlice anchor = ScanToSpace(ref args1);
		StringSlice script = ScanToSpace(ref args1);

		return new()
		{
			Args = args,
			Name = name,
			Anchor = anchor.ToString(),
			Script = script.IsEmpty ? null : script.ToString()
		};
	}
}
