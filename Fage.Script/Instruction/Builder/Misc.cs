using Fage.Script.Instruction.Instantization;
using Fage.Script.Serialization;
using System.Numerics;


namespace Fage.Script.Instruction;

public partial class InstructionSequenceBuilder
{
	public InstructionSequenceBuilder Nop()
	{
		Sequence.Add(new SerializedInstruction<ParameterLess>
		{
			Opcode = ScriptInstructionOpcode.Nop,
			InstantizationInfo = ParameterLess.Instance
		});

		return this;
	}

	public InstructionSequenceBuilder ReturnToTitle()
	{
		Sequence.Add(new SerializedInstruction<ParameterLess>
		{
			Opcode = ScriptInstructionOpcode.ReturnToTitle,
			InstantizationInfo = ParameterLess.Instance
		});

		return this;
	}

	public InstructionSequenceBuilder Code(bool isBlock, string content, int? lineNumber, (int StartPosition, int EndPosition)? span)
	{
		Sequence.Add(new SerializedInstruction<Code>
		{
			Opcode = ScriptInstructionOpcode.Code,
			InstantizationInfo = new Code
			{
				IsBlock = isBlock,
				Content = content,
				LineNumber = lineNumber,
				Span = span
			}
		});

		return this;
	}
}