using Fage.Script.Instruction;
using Markdig.Helpers;
using Markdig.Syntax;
using System.Text;

namespace Fage.Script.Compiler.Renderer.Blocks;

public class CodeBlockRenderer : FageObjectRendererBase<FencedCodeBlock>
{
	private string BuildCodeString(StringLineGroup lines)
	{
		int length = 0;
		for (int i = 0; i < lines.Count; i++)
		{
			// 行长度 + CR LF
			length += lines.Lines[i].Slice.Length + 2;
		}

		StringBuilder sb = new(length);

		for (int i = 0; i < lines.Count; i++)
		{
			sb.Append(lines.Lines[i].Slice.AsSpan())
				.AppendLine();
		}

		return sb.ToString();
	}

	protected override void FageEmit(FageScriptRenderer renderer, InstructionSequenceBuilder builder, FencedCodeBlock obj)
	{
		builder.Code(true, BuildCodeString(obj.Lines), obj.Line, (obj.Span.Start, obj.Span.End));
	}
}
