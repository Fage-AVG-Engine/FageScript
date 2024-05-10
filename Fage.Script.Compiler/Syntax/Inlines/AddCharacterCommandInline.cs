namespace Fage.Script.Compiler.Syntax.Inlines;

public class AddCharacterCommandInline : FageCommandInline
{
	public required string CharacterId { get; init; }
	public string? Motion { get; init; } = null;
	public int? Position { get; init; } = null;
}
