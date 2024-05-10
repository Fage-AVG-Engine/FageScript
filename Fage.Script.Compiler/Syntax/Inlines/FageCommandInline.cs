using Markdig.Helpers;
using Markdig.Syntax.Inlines;

namespace Fage.Script.Compiler.Syntax.Inlines;

public class FageCommandInline : LeafInline
{
	public required StringSlice Name { get; init; }
	public required StringSlice Args { get; init; }
	public bool ArgsMissing { get; init; } = false;

	public override string ToString()
	{
		return $"{Name} Args => {Args}";
	}
}
