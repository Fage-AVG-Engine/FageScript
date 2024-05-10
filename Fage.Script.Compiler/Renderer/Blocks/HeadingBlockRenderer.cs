using Fage.Script.Instruction;
using Markdig.Syntax;
using Markdig.Syntax.Inlines;

namespace Fage.Script.Compiler.Renderer.Blocks;

public class HeadingBlockRenderer : FageObjectRendererBase<HeadingBlock>
{
	protected override void FageEmit(FageScriptRenderer renderer, InstructionSequenceBuilder builder, HeadingBlock obj)
	{
		var firstInline = obj.Inline?.FirstChild;
		var firstLiteral = ScanInlineOfType<LiteralInline>(firstInline);
		if (firstLiteral == null)
			return;

		string id = MergeContinuousLiteral(ref firstLiteral);

		builder.Anchor(id, id);

		return;
	}
}
