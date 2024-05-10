using Fage.Script.Instruction.Instantization;
using Fage.Script.Serialization;


namespace Fage.Script.Instruction;

public partial class InstructionSequenceBuilder
{
	public InstructionSequenceBuilder SetBackground(string backgroundName)
	{
		Sequence.Add(new SerializedInstruction<SetBackground>
		{
			Opcode = ScriptInstructionOpcode.SetBackground,
			InstantizationInfo = new SetBackground
			{
				Name = backgroundName
			}
		});

		return this;
	}
}