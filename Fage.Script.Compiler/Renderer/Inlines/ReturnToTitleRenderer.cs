using Fage.Script.Compiler.Syntax.Inlines;
using Fage.Script.Instruction;

namespace Fage.Script.Compiler.Renderer.Inlines;

public class ReturnToTitleRenderer : FageObjectRendererBase<TitleCommandInline>
{
	protected override void FageEmit(FageScriptRenderer renderer, InstructionSequenceBuilder builder, TitleCommandInline obj)
	{
		builder.ReturnToTitle();
	}
}

