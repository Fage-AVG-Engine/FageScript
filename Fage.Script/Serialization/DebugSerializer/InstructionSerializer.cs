using Fage.Script.Instruction;
using Fage.Script.Instruction.Instantization;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

namespace Fage.Script.Serialization.DebugSerializer;

internal class InstructionSerializer<TInfo> : JsonConverter<ISerializedInstruction<TInfo>>
	where TInfo : class, IInstructionInstantization
{

	private static JsonTypeInfo<ScriptInstructionOpcode> GetOpcodeTypeInfo(JsonSerializerOptions options)
		=> (JsonTypeInfo<ScriptInstructionOpcode>)options.GetTypeInfo(typeof(ScriptInstructionOpcode));

	public override ISerializedInstruction<TInfo>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		return (ISerializedInstruction<TInfo>?)JsonSerializer.Deserialize(ref reader, options.GetTypeInfo(typeof(ISerializedInstruction)));
	}

	public override void Write(Utf8JsonWriter writer, ISerializedInstruction<TInfo> value, JsonSerializerOptions options)
	{
		var opcodeTypeInfo = GetOpcodeTypeInfo(options);

		writer.WriteStartObject();

		writer.WritePropertyName(SerializedInstructionJsonConverterFactory.OpcodeJsonName);
		JsonSerializer.Serialize(writer, value.Opcode, opcodeTypeInfo);

		writer.WritePropertyName(SerializedInstructionJsonConverterFactory.InfoJsonName);
		JsonSerializer.Serialize(writer, value.InstantizationInfo, options.GetTypeInfo(typeof(TInfo)));

		writer.WriteEndObject();
	}
}
