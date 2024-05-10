using Fage.Script.Instruction;
using Fage.Script.Instruction.Instantization;
using Fage.Script.Serialization.DebugSerializer;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Fage.Script.Serialization;

[JsonConverter(typeof(SerializedInstructionJsonConverterFactory))]
[DebuggerDisplay("{DebuggerDisplay,nq}")]
public class SerializedInstruction<T> : ISerializedInstruction<T>
	where T : class, IInstructionInstantization
{
	public ScriptInstructionOpcode Opcode { get; set; }

	public required T InstantizationInfo { get; set; }

	IInstructionInstantization ISerializedInstruction.InstantizationInfo => InstantizationInfo;

	private string DebuggerDisplay
	{
		get
		{
			try
			{
				return $"{Opcode}, {JsonSerializer.Serialize(InstantizationInfo, FageScriptJsonContext.Default.Options)}";
			}
			catch (JsonException)
			{
				return $"Opcode {Opcode}, {InstantizationInfo}";
			}
		}
	}
}