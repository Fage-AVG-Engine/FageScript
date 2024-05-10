using Fage.Script.Instruction;
using Fage.Script.Instruction.Instantization;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection.PortableExecutable;
using System.Runtime;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Fage.Script.Serialization.DebugSerializer
{
	internal partial class SerializedInstructionJsonConverterFactory
		: JsonConverterFactory
	{
		internal static readonly Dictionary<ScriptInstructionOpcode, Type> OpcodeInfoTypeMap = new(32)
		{
			[ScriptInstructionOpcode.Nop] = typeof(ParameterLess),

			[ScriptInstructionOpcode.Dialogue] = typeof(Dialogue),
			// [ScriptInstructionOpcode.ParagraphEnd] = typeof(ParameterLess), 已移除
			[ScriptInstructionOpcode.ParagraphStart] = typeof(ParameterLess),

			[ScriptInstructionOpcode.Code] = typeof(Code),

			[ScriptInstructionOpcode.AddCharacter] = typeof(AddCharacter),
			[ScriptInstructionOpcode.RemoveCharacter] = typeof(RemoveCharacter),
			[ScriptInstructionOpcode.SetSpeakingCharacter] = typeof(SetSpeakingCharacter),

			[ScriptInstructionOpcode.Anchor] = typeof(Anchor),
			[ScriptInstructionOpcode.JumpToAnchor] = typeof(JumpToAnchor),

			[ScriptInstructionOpcode.SetBackground] = typeof(SetBackground),

			[ScriptInstructionOpcode.SetBgm] = typeof(SetBgm),
			[ScriptInstructionOpcode.StartVoice] = typeof(StartVoice),
			[ScriptInstructionOpcode.StartSfx] = typeof(StartSfx),

			[ScriptInstructionOpcode.Branch] = typeof(Branch),

			[ScriptInstructionOpcode.ReturnToTitle] = typeof(ParameterLess)
		};

		internal static readonly JsonEncodedText OpcodeJsonName = JsonEncodedText.Encode(nameof(ISerializedInstruction.Opcode));
		internal static readonly JsonEncodedText InfoJsonName = JsonEncodedText.Encode(nameof(ISerializedInstruction.InstantizationInfo));


		internal static readonly Type[] SupportedInfoTypes = GetSupportedInfoTypes(OpcodeInfoTypeMap);

		internal static readonly Dictionary<Type, Type> SerializerMap = new(32)
		{
			[typeof(ISerializedInstruction<ParameterLess>)] = typeof(InstructionSerializer<ParameterLess>),

			[typeof(ISerializedInstruction<Dialogue>)] = typeof(InstructionSerializer<Dialogue>),

			[typeof(ISerializedInstruction<SetBackground>)] = typeof(InstructionSerializer<SetBackground>),

			[typeof(ISerializedInstruction<AddCharacter>)] = typeof(InstructionSerializer<AddCharacter>),
			[typeof(ISerializedInstruction<RemoveCharacter>)] = typeof(InstructionSerializer<RemoveCharacter>),

			[typeof(ISerializedInstruction<Anchor>)] = typeof(InstructionSerializer<Anchor>),
			[typeof(ISerializedInstruction<JumpToAnchor>)] = typeof(InstructionSerializer<JumpToAnchor>),

			[typeof(ISerializedInstruction<SetBgm>)] = typeof(InstructionSerializer<SetBgm>),
			[typeof(ISerializedInstruction<StartVoice>)] = typeof(InstructionSerializer<StartVoice>),
			[typeof(ISerializedInstruction<StartSfx>)] = typeof(InstructionSerializer<StartSfx>)
		};

		private static Type[] GetSupportedInfoTypes(Dictionary<ScriptInstructionOpcode, Type> opcodeInfoTypeMap)
		{
			return [.. OpcodeInfoTypeMap.Values];
		}

		private static bool FilterOutTypedInstruction(Type t, object? unused)
		{
			Debug.Assert(t.IsInterface, "t不是接口类型");

			if (!t.IsConstructedGenericType)
				return false;

			var genericDefinition = t.GetGenericTypeDefinition();
			if (genericDefinition != typeof(ISerializedInstruction<>))
				return false;

			var genericArgs = t.GetGenericArguments();
			if (genericArgs.Length != 1)
				return false;

			return SupportedInfoTypes.Contains(genericArgs[0]);
		}

		public override bool CanConvert(Type typeToConvert)
		{
			if (typeToConvert == typeof(ISerializedInstruction)) 
				return true;

			if (typeToConvert.IsGenericType 
				&& typeToConvert.GetGenericTypeDefinition() == typeof(SerializedInstruction<>)
				&& SupportedInfoTypes.Contains(typeToConvert.GenericTypeArguments[0]))
				return true;

			var interfaces = typeToConvert.GetInterfaces();

			if (!interfaces.Contains(typeof(ISerializedInstruction)))
				return false;

			Type[] typedInstructionInterfaces = GetInstructionInterfaces(typeToConvert);

			return typedInstructionInterfaces.Length == 1;
		}

		internal static Type[] GetInstructionInterfaces(Type typeToConvert)
		{
			return typeToConvert.FindInterfaces(FilterOutTypedInstruction, null);
		}

		public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options)
		{
			if (!CanConvert(typeToConvert)) return null;

			if (typeToConvert == typeof(ISerializedInstruction))
				return new InstructionDeserializer();

			var typeUsing = GetInstructionInterfaces(typeToConvert)[0];

			if (SerializerMap.TryGetValue(typeUsing, out var knownConverter))
			{
				return (JsonConverter)Activator.CreateInstance(knownConverter)!;
			} 
			else
			{
				Type converterType;

				try
				{
					converterType = typeof(InstructionSerializer<>)
								.MakeGenericType(typeUsing.GenericTypeArguments[0]);
				}
				catch (ArgumentException)
				{
					// 不符合泛型约束
					return null;
				}

				return (JsonConverter)Activator.CreateInstance(converterType)!;
			}	
		}
	}
}
