using Fage.Script.Instruction.Instantization;
using Fage.Script.Serialization;

namespace Fage.Script.Instruction;

public partial class InstructionSequenceBuilder
{
	public InstructionSequenceBuilder Dialogue(string content, bool completesParagraph = false)
	{
		Sequence.Add(new SerializedInstruction<Dialogue> 
		{
			Opcode = ScriptInstructionOpcode.Dialogue,
			InstantizationInfo = new Dialogue { Content = content, CompletesParagraph = completesParagraph }
		});
		return this;
	}

	public InstructionSequenceBuilder ParagraphStart()
	{
		Sequence.Add(new SerializedInstruction<ParameterLess>
		{
			Opcode = ScriptInstructionOpcode.ParagraphStart,
			InstantizationInfo = ParameterLess.Instance
		});
		return this;
	}
}