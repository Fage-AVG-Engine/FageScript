using Fage.Script.Instruction;
using Fage.Script.Instruction.Builder;
using Markdig.Syntax;
using Markdig.Syntax.Inlines;

namespace Fage.Script.Compiler.Renderer.Blocks;

public class ListBlockRenderer : FageObjectRendererBase<ListBlock>
{
	private void EmitOption(BranchOptionsBuilder builder, ParagraphBlock paragraph)
	{
		ContainerInline? container = paragraph.Inline;
		if (container == null)
		{
			// TODO Emit Diagnose
			return;
		}

		LiteralInline? firstLiteral = ScanNextInlineOfType<LiteralInline>(container.FirstChild);
		if (firstLiteral == null)
		{
			// TODO emit diagnose
			return;
		}

		string hintText = MergeContinuousLiteral(ref firstLiteral);
		hintText = hintText.Trim();

		var firstLineBreak = ScanNextInlineOfType<LineBreakInline>(firstLiteral);

		if (firstLineBreak == null)
		{
			builder.Nop(hintText);
			return;
		}

		var code = ScanNextInlineOfType<CodeInline>(firstLineBreak);
		if (code != null)
		{
			builder.Code(hintText, code.Content);
			return;
		}

		var singleLineLiteral = ScanNextInlineOfType<LiteralInline>(firstLineBreak);
		if (singleLineLiteral != null)
		{
			string anchorExpression = MergeContinuousLiteral(ref singleLineLiteral);
			builder.Jump(hintText, anchorExpression.TrimStart());
		}
		else
		{
			builder.Nop(hintText);
		}
	}

	protected override void FageEmit(FageScriptRenderer renderer, InstructionSequenceBuilder builder, ListBlock obj)
	{
		BranchOptionsBuilder optionsBuilder = new();

		foreach (ListItemBlock item in obj.OfType<ListItemBlock>())
		{
			for (int i = 0; i < item.Count; i++)
			{
				if (item[i] is ParagraphBlock paragraph)
				{
					EmitOption(optionsBuilder, paragraph);
				}
			}
		}

		builder.Branch(optionsBuilder.Options);
	}
}
