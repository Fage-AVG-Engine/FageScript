using Fage.Script.Instruction.Instantization;
using Fage.Script.Serialization;


namespace Fage.Script.Instruction;

public partial class InstructionSequenceBuilder
{
	public InstructionSequenceBuilder Branch(IEnumerable<Branch.BranchOption> options)
	{
		Sequence.Add(new SerializedInstruction<Branch>()
		{
			Opcode = ScriptInstructionOpcode.Branch,
			InstantizationInfo = new Branch
			{
				Options = [.. options]
			}
		});

		return this;
	}
}