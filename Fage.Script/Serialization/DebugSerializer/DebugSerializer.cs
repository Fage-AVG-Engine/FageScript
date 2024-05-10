using Fage.Script.Instruction.Instantization;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

namespace Fage.Script.Serialization.DebugSerializer;

public class DebugSerializer : IFageScriptSerializer
{
	public static ScriptEncoding ScriptEncoding { get; } = ScriptEncoding.DebugJson;
	public static Version InstructionEncodingVersion { get; } = new("1.0.0");
	public static int FormatVersion { get; } = 0;

	public static readonly DebugSerializer Default = new();

	private static readonly JsonSerializerOptions _jsonOptions = GetJsonOptions();
	private static JsonSerializerOptions GetJsonOptions()
	{
		JsonSerializerOptions options = new(FageScriptJsonContext.Default.Options)
		{
			Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
		};
		return options;
	}

	public FageScriptInstantization Deserialize(Stream stream)
	{
		return JsonSerializer.Deserialize<FageScriptInstantization>(
			stream,
			_jsonOptions
		) ?? throw new InvalidDataException("脚本内容为 null");
	}

	public void Serialize(FageScriptInstantization script, Stream output)
	{
		script = PrepareForSerialization(script);

		JsonSerializer.Serialize(
			output,
			script,
			_jsonOptions
		);
	}

	public bool IsEncodingSupported(ScriptEncoding encoding)
		=> encoding == ScriptEncoding.DebugJson;

	public static Task SerializeAsync(
		FageScriptInstantization script,
		Stream output,
		CancellationToken cancellationToken = default
	)
	{
		script = PrepareForSerialization(script);

		return JsonSerializer.SerializeAsync(
			output,
			script,
			_jsonOptions,
			cancellationToken
		);
	}

	public static async ValueTask<FageScriptInstantization> DeserializeAsync(Stream stream, CancellationToken cancellationToken = default)
	{
		return await JsonSerializer.DeserializeAsync<FageScriptInstantization>(stream, _jsonOptions, cancellationToken)
			?? throw new InvalidDataException("脚本内容只包含 null");
	}
	private static FageScriptInstantization PrepareForSerialization(FageScriptInstantization script)
	{
		if (script.ScriptEncoding == ScriptEncoding
			&& script.FormatVersion == FormatVersion
			&& script.InstructionEncodingVersion == InstructionEncodingVersion)
		{
			return script;
		}

		return new()
		{
			InstructionSequence = script.InstructionSequence,
			FormatVersion = FormatVersion,
			ScriptEncoding = ScriptEncoding,
			InstructionEncodingVersion = InstructionEncodingVersion
		};
	}
}
