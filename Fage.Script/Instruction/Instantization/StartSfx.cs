namespace Fage.Script.Instruction.Instantization;

public class StartSfx : IInstructionInstantization
{
	public required string Name { get; set; }

	public float? Volume { get; set; } = 1.0f;
}
