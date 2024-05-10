namespace Fage.Script.Compiler.Syntax.Inlines;

public class JumpCommandInline : FageCommandInline
{
	public required string Anchor { get; init; }
	public string? Script { get; init; }
}
