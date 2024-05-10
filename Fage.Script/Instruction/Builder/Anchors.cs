using Fage.Script.Instruction.Instantization;
using Fage.Script.Serialization;

namespace Fage.Script.Instruction;

public partial class InstructionSequenceBuilder
{
	public InstructionSequenceBuilder Anchor(string name, string? headingText = null)
	{
		Sequence.Add(new SerializedInstruction<Anchor>
		{
			Opcode = ScriptInstructionOpcode.Anchor,
			InstantizationInfo = new Anchor
			{
				Name = name,
				HeadingText = headingText,
			}
		});
		return this;
	}

	public InstructionSequenceBuilder JumpToAnchor(string anchor, string? script)
	{
		Sequence.Add(new SerializedInstruction<JumpToAnchor>
		{
			Opcode = ScriptInstructionOpcode.JumpToAnchor,
			InstantizationInfo = new JumpToAnchor
			{
				Anchor = anchor,
				Script = script
			}
		});
		return this;
	}
}
