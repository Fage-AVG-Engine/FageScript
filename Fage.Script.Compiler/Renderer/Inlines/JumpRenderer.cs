using Fage.Script.Compiler.Syntax.Inlines;
using Fage.Script.Instruction;

namespace Fage.Script.Compiler.Renderer.Inlines;

public class JumpRenderer : FageObjectRendererBase<JumpCommandInline>
{
	protected override void FageEmit(FageScriptRenderer renderer, InstructionSequenceBuilder builder, JumpCommandInline obj)
	{
		builder.JumpToAnchor(obj.Anchor, obj.Script);
	}
}
