namespace Fage.Script.Instruction.Instantization;

public class AddCharacter : IInstructionInstantization
{
	public required string Identity { get; set; }
	public string? Motion { get; set; }
	public int? Position { get; set; }
}
