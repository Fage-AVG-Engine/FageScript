using Fage.Script.Instruction;
using Markdig.Syntax;

namespace Fage.Script.Compiler.Renderer.Blocks;

public class QuoteRenderer : FageObjectRendererBase<QuoteBlock>
{
	protected override void FageEmit(FageScriptRenderer renderer, InstructionSequenceBuilder builder, QuoteBlock obj)
	{
		// 是注释，不需要发出指令
		return;
	}
}