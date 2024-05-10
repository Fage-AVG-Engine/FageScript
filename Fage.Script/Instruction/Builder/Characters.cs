using Fage.Script.Instruction.Instantization;
using Fage.Script.Serialization;


namespace Fage.Script.Instruction;

public partial class InstructionSequenceBuilder
{
	public InstructionSequenceBuilder AddCharacter(string identity, string? motion = null, int? position = null)
	{
		Sequence.Add(new SerializedInstruction<AddCharacter>
		{
			Opcode = ScriptInstructionOpcode.AddCharacter,
			InstantizationInfo = new AddCharacter
			{
				Identity = identity,
				Motion = motion,
				Position = position
			}
		});

		return this;
	}

	public InstructionSequenceBuilder RemoveCharacter(string identity, bool tryUnloadResources = false)
	{
		Sequence.Add(new SerializedInstruction<RemoveCharacter>
		{
			Opcode = ScriptInstructionOpcode.RemoveCharacter,
			InstantizationInfo = new RemoveCharacter
			{
				Identity = identity,
				TryUnloadResources = tryUnloadResources
			}
		});

		return this;
	}

	public InstructionSequenceBuilder SetSpeakingCharacter(string identity)
	{
		Sequence.Add(new SerializedInstruction<SetSpeakingCharacter>
		{
			Opcode = ScriptInstructionOpcode.SetSpeakingCharacter,
			InstantizationInfo = new SetSpeakingCharacter
			{
				Identity = identity
			}
		});

		return this;
	}
}