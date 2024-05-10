namespace Fage.Script.Compiler.Syntax.Inlines;

public class SfxCommandInline : FageCommandInline
{
	public required string SoundEffect { get; init; }
	public float? Volume { get; init; }
}
