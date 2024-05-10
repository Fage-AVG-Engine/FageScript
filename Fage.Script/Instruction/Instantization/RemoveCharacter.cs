namespace Fage.Script.Instruction.Instantization;

public class RemoveCharacter : IInstructionInstantization
{
	public required string Identity { get; set; }
	public bool TryUnloadResources { get; set; } = false;
}
