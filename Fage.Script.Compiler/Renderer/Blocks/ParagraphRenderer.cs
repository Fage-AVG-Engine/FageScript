using Fage.Script.Instruction;
using Markdig.Syntax;
using Markdig.Syntax.Inlines;

namespace Fage.Script.Compiler.Renderer.Blocks;

public class ParagraphRenderer : FageObjectRendererBase<ParagraphBlock>
{
	protected override void FageEmit(FageScriptRenderer renderer, InstructionSequenceBuilder builder, ParagraphBlock obj)
	{
		if (obj.Parent!.Parent != null)
			return;

		ContainerInline? container = obj.Inline;
		if (container == null)
			return;

		Inline? inline = container.FirstChild;
		if (ScanInlineOfType<LiteralInline>(inline) != null)
		{
			builder.ParagraphStart();
		}

		while (inline != null)
		{
			if (inline is LiteralInline literal)
			{
				string dialogContent = MergeContinuousLiteral(ref literal);
				// Literal, <null> => Literal是最后一个Inline，但后面没有换行，认为是结尾
				// Literal, LineBreak, <null> => Literal接LineBreak，下面没了，是结尾
				bool lastItem = literal.NextSibling == null ||
					literal.NextSibling is LineBreakInline lineBreak && ScanInlineOfType<LiteralInline>(lineBreak.NextSibling) == null;

				builder.Dialogue(dialogContent, lastItem);

				inline = literal;
			}
			else
			{
				renderer.Write(inline);
			}

			inline = inline.NextSibling;
		}
	}
}
