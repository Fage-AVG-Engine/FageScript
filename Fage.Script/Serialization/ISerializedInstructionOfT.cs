using Fage.Script.Instruction.Instantization;

namespace Fage.Script.Serialization;

public interface ISerializedInstruction<T> : ISerializedInstruction
	where T : class, IInstructionInstantization
{
	new T InstantizationInfo { get; }
}