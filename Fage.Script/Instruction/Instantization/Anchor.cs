namespace Fage.Script.Instruction.Instantization;

public class Anchor : IInstructionInstantization
{
	public required string Name { get; set; }
	public string? HeadingText { get; set; }
}
