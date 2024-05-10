using Fage.Script.Instruction.Instantization;
using Fage.Script.Serialization;


namespace Fage.Script.Instruction;

public partial class InstructionSequenceBuilder
{
	public InstructionSequenceBuilder SetBgm(string bgmName)
	{
		Sequence.Add(new SerializedInstruction<SetBgm>
		{
			Opcode = ScriptInstructionOpcode.SetBgm,
			InstantizationInfo = new SetBgm { Name = bgmName }
		});

		return this;
	}

	public InstructionSequenceBuilder StartVoice(string voiceName)
	{
		Sequence.Add(new SerializedInstruction<StartVoice>
		{
			Opcode = ScriptInstructionOpcode.StartVoice,
			InstantizationInfo = new StartVoice { Name = voiceName }
		});

		return this;
	}

	public InstructionSequenceBuilder StartSfx(string sfxName, float? volume)
	{
		Sequence.Add(new SerializedInstruction<StartSfx>
		{
			Opcode = ScriptInstructionOpcode.StartSfx,
			InstantizationInfo = new StartSfx
			{
				Name = sfxName,
				Volume = volume
			}
		});

		return this;
	}
}