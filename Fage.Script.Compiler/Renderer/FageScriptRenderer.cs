using Fage.Script.Instruction;
using Fage.Script.Serialization;
using Markdig.Renderers;
using Markdig.Syntax;

namespace Fage.Script.Compiler.Renderer;

public class FageScriptRenderer(InstructionSequenceBuilder builder) : RendererBase
{
	public InstructionSequenceBuilder SequenceBuilder { get; } = builder;

	public override sealed object Render(MarkdownObject markdownObject)
	{
		Write(markdownObject);
		return SequenceBuilder;
	}

	public virtual IReadOnlyList<ISerializedInstruction> CompleteAndReset()
	{
		var result = SequenceBuilder.Build();
		SequenceBuilder.Reset();
		return result;
	}
}
