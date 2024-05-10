namespace Fage.Script.Serialization;

public interface IFageScriptSerializer
{
	public static abstract ScriptEncoding ScriptEncoding { get; }
	public static abstract Version InstructionEncodingVersion { get; }
	public static abstract int FormatVersion { get; }
	public FageScriptInstantization Deserialize(Stream stream);
	bool IsEncodingSupported(ScriptEncoding encoding);
	public void Serialize(FageScriptInstantization script, Stream output);
}
