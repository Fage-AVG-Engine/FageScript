namespace Fage.Script.Serialization
{
	public class FageScriptInstantization
	{
		public static readonly Version CurrentInstructionSetVersion = new("0.1.0");

		public Version InstructionSetVersion { get; init; } = CurrentInstructionSetVersion;

		public int FormatVersion { get; init; }
		public ScriptEncoding ScriptEncoding { get; init; }
		public required Version InstructionEncodingVersion { get; init; }
		public required IReadOnlyList<ISerializedInstruction> InstructionSequence { get; init; }
	}
}
