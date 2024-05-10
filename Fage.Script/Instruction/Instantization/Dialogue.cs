using System.Text.Json.Serialization;

namespace Fage.Script.Instruction.Instantization;

public class Dialogue : IInstructionInstantization
{
	public required string Content { get; set; }

	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
	public bool CompletesParagraph { get; set; } = false;
}
