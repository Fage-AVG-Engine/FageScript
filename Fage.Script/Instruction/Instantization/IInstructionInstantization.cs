using System.Text.Json.Serialization;

namespace Fage.Script.Instruction.Instantization;

[JsonPolymorphic]
[JsonDerivedType(typeof(AddCharacter))]
[JsonDerivedType(typeof(Anchor))]
[JsonDerivedType(typeof(Code))]
[JsonDerivedType(typeof(Dialogue))]
[JsonDerivedType(typeof(JumpToAnchor))]
[JsonDerivedType(typeof(ParameterLess))]
[JsonDerivedType(typeof(RemoveCharacter))]
[JsonDerivedType(typeof(SetBackground))]
[JsonDerivedType(typeof(SetBgm))]
[JsonDerivedType(typeof(SetSpeakingCharacter))]
[JsonDerivedType(typeof(StartSfx))]
[JsonDerivedType(typeof(StartVoice))]
[JsonDerivedType(typeof(Branch))]
public interface IInstructionInstantization
{
}
