namespace Fage.Script.Instruction.Instantization;

public class Branch : IInstructionInstantization
{
	public record BranchOption(string HintText, string? AnchorExpression, string? Code);

	public required BranchOption[] Options { get; set; }
}
