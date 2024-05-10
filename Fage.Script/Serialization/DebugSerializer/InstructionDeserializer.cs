using Fage.Script.Instruction;
using Fage.Script.Instruction.Instantization;
using System.Diagnostics;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Fage.Script.Serialization.DebugSerializer;

internal class InstructionDeserializer : JsonConverter<ISerializedInstruction>
{
	private static readonly Type InstructionType = typeof(SerializedInstruction<>);

	private static readonly MethodInfo SetterMethodInfo = typeof(InstructionDeserializer)
		.GetMethod(nameof(SetMemberTyped), BindingFlags.NonPublic | BindingFlags.Instance)!;

	private void SetMemberTyped<TInfo>(SerializedInstruction<TInfo> instruction, ScriptInstructionOpcode opcode, TInfo info)
		where TInfo : class, IInstructionInstantization
	{
		instruction.Opcode = opcode;
		instruction.InstantizationInfo = info;
	}

	private ISerializedInstruction Create(ScriptInstructionOpcode opcode, object info, Type infoType)
	{
		var instructionType = InstructionType.MakeGenericType(infoType);
		ISerializedInstruction instruction = (ISerializedInstruction)Activator.CreateInstance(instructionType)!;

		SetterMethodInfo.MakeGenericMethod(infoType).Invoke(this, [instruction, opcode, info]);
		return instruction;
	}

	public override ISerializedInstruction? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		if (reader.TokenType != JsonTokenType.StartObject)
			throw new JsonException($"序列化的 {nameof(ISerializedInstruction)} 必须是JSON对象");

		reader.Read();
		if (reader.TokenType != JsonTokenType.PropertyName)
			throw new JsonException();

		if (!reader.ValueTextEquals(SerializedInstructionJsonConverterFactory.OpcodeJsonName.EncodedUtf8Bytes))
			throw new JsonException($"{nameof(ISerializedInstruction)}的第一个属性必须是\"{nameof(ISerializedInstruction.Opcode)}\"");

		var opcodeTypeInfo = options.GetTypeInfo(typeof(ScriptInstructionOpcode));
		ScriptInstructionOpcode opcode = (ScriptInstructionOpcode)(JsonSerializer.Deserialize(ref reader, opcodeTypeInfo)
			?? throw new JsonException($"{nameof(ISerializedInstruction.Opcode)} 属性不能为 null。"));

		Type infoType = SerializedInstructionJsonConverterFactory.OpcodeInfoTypeMap[opcode];

		reader.Read();
		if (reader.TokenType != JsonTokenType.PropertyName
			|| !reader.ValueTextEquals(SerializedInstructionJsonConverterFactory.InfoJsonName.EncodedUtf8Bytes))
			throw new JsonException($"指令缺少{nameof(ISerializedInstruction.InstantizationInfo)}属性。");
		
		object instantizationInfo;
		if (infoType != typeof(ParameterLess))
		{
			instantizationInfo = JsonSerializer.Deserialize(ref reader, options.GetTypeInfo(infoType))
				?? throw new JsonException($"{nameof(ISerializedInstruction.InstantizationInfo)} 属性为 null。");
		}
		else
		{
			reader.Read();
			if (reader.TokenType == JsonTokenType.StartObject)
			{
				reader.Skip();
				Debug.Assert(reader.TokenType == JsonTokenType.EndObject);
			}
			else if (reader.TokenType == JsonTokenType.Null)
			{
				
			}
			else
			{
				throw new JsonException($"{SerializedInstructionJsonConverterFactory.InfoJsonName.Value}的值必须是JSON Object");
			}

			instantizationInfo = ParameterLess.Instance;
		}

		reader.Read();
		return Create(opcode, instantizationInfo, infoType);
	}

	public override void Write(Utf8JsonWriter writer, ISerializedInstruction value, JsonSerializerOptions options)
	{
		var typedInstructionInterface = SerializedInstructionJsonConverterFactory.GetInstructionInterfaces(value.GetType());
		if (typedInstructionInterface.Length == 1)
		{
			JsonSerializer.Serialize(writer, value, options.GetTypeInfo(value.GetType()));
		}
		else
		{
			throw new NotSupportedException();
		}
	}
}
