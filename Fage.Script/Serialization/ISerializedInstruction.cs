using Fage.Script.Instruction;
using Fage.Script.Instruction.Instantization;
using Fage.Script.Serialization.DebugSerializer;
using System.Text.Json.Serialization;

namespace Fage.Script.Serialization;

[JsonConverter(typeof(InstructionDeserializer))]
public interface ISerializedInstruction
{
	public ScriptInstructionOpcode Opcode { get; }

	public IInstructionInstantization InstantizationInfo { get; }
}
