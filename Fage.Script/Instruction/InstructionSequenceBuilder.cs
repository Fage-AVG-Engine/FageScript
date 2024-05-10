using Fage.Script.Instruction.Instantization;
using Fage.Script.Serialization;

namespace Fage.Script.Instruction;

public partial class InstructionSequenceBuilder
{
	protected List<ISerializedInstruction> Sequence { get; } = new(64);

	public IReadOnlyList<ISerializedInstruction> Build() => [.. Sequence];

	public void Reset() => Sequence.Clear();

	public InstructionSequenceBuilder SpecialInstruction<TInstantization>(ScriptInstructionOpcode opcode, TInstantization instantization)
		where TInstantization: class, IInstructionInstantization
	{
		Sequence.Add(new SerializedInstruction<TInstantization>
		{
			Opcode = opcode,
			InstantizationInfo = instantization
		});

		return this;
	}
}
