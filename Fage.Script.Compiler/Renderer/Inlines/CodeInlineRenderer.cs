using Fage.Script.Instruction;
using Markdig.Syntax.Inlines;

namespace Fage.Script.Compiler.Renderer.Inlines;

public class CodeInlineRenderer : FageObjectRendererBase<CodeInline>
{
	protected override void FageEmit(FageScriptRenderer renderer, InstructionSequenceBuilder builder, CodeInline obj)
	{
		builder.Code(false, obj.Content, obj.Line + 1, (obj.Span.Start, obj.Span.End));
	}
}
