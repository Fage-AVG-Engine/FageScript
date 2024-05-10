using Fage.Script.Instruction;
using Fage.Script.Instruction.Instantization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Fage.Script.Serialization.DebugSerializer;

[JsonSerializable(typeof(FageScriptInstantization))]
[JsonSerializable(typeof(ScriptInstructionOpcode))]
[JsonSerializable(typeof(ParameterLess))]
[JsonSerializable(typeof(SerializedInstruction<ParameterLess>))]
[JsonSerializable(typeof(Anchor))]
[JsonSerializable(typeof(SerializedInstruction<Anchor>))]
[JsonSerializable(typeof(AddCharacter))]
[JsonSerializable(typeof(SerializedInstruction<AddCharacter>))]
[JsonSerializable(typeof(Code))]
[JsonSerializable(typeof(SerializedInstruction<Code>))]
[JsonSerializable(typeof(Dialogue))]
[JsonSerializable(typeof(SerializedInstruction<Dialogue>))]
[JsonSerializable(typeof(JumpToAnchor))]
[JsonSerializable(typeof(SerializedInstruction<JumpToAnchor>))]
[JsonSerializable(typeof(RemoveCharacter))]
[JsonSerializable(typeof(SerializedInstruction<RemoveCharacter>))]
[JsonSerializable(typeof(SetBackground))]
[JsonSerializable(typeof(SerializedInstruction<SetBackground>))]
[JsonSerializable(typeof(SetBgm))]
[JsonSerializable(typeof(SerializedInstruction<SetBgm>))]
[JsonSerializable(typeof(SetSpeakingCharacter))]
[JsonSerializable(typeof(SerializedInstruction<SetSpeakingCharacter>))]
[JsonSerializable(typeof(StartVoice))]
[JsonSerializable(typeof(SerializedInstruction<StartVoice>))]
[JsonSerializable(typeof(Branch))]
[JsonSerializable(typeof(SerializedInstruction<Branch>))]
[JsonSourceGenerationOptions(IncludeFields = true, UseStringEnumConverter = true, WriteIndented = true,
	ReadCommentHandling = JsonCommentHandling.Skip, AllowTrailingCommas = true)]
public partial class FageScriptJsonContext : JsonSerializerContext
{

}
