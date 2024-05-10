using Fage.Script.Compiler.Syntax.Inlines;
using Markdig.Helpers;

namespace Fage.Script.Compiler.Parsers
{
	public class UnknownCommandParser : FageCommandInlineParser<FageCommandInline>
	{
		public UnknownCommandParser() : base("")
		{
			ArgsRequired = false;
		}

		protected override FageCommandInline CreateInlineSyntax(StringSlice name, StringSlice args)
		{
			return new FageCommandInline() { Args = args, Name = name };
		}
	}
}
