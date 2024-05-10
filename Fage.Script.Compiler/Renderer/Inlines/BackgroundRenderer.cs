using Fage.Script.Compiler.Syntax.Inlines;
using Fage.Script.Instruction;

namespace Fage.Script.Compiler.Renderer.Inlines;

public class BackgroundRenderer : FageObjectRendererBase<BackgroundCommandInline>
{
	protected override void FageEmit(FageScriptRenderer renderer, InstructionSequenceBuilder builder, BackgroundCommandInline obj)
	{
		builder.SetBackground(obj.Background);
	}
}
