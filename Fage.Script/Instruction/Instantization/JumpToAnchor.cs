namespace Fage.Script.Instruction.Instantization;

public class JumpToAnchor : IInstructionInstantization
{
	public required string Anchor { get; set; }
	public string? Script { get; set; }
}
