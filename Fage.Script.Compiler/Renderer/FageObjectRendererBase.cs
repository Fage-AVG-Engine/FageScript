using Fage.Script.Instruction;
using Markdig.Renderers;
using Markdig.Syntax;
using Markdig.Syntax.Inlines;
using System.Text;

namespace Fage.Script.Compiler.Renderer;

public abstract class FageObjectRendererBase<TSyntax> : MarkdownObjectRenderer<FageScriptRenderer, TSyntax>
	where TSyntax : MarkdownObject
{
	protected sealed override void Write(FageScriptRenderer renderer, TSyntax obj)
	{
		FageEmit(renderer, renderer.SequenceBuilder, obj);
	}

	protected abstract void FageEmit(FageScriptRenderer renderer, InstructionSequenceBuilder builder, TSyntax obj);

	public static string MergeContinuousLiteral(ref LiteralInline literalNode)
	{
		if (literalNode.NextSibling is not LiteralInline nextLiteral)
			return literalNode.Content.ToString();

		StringBuilder sb = new(literalNode.Content.ToString());

		LiteralInline? nextInline = nextLiteral;

		while (nextInline != null)
		{
			literalNode = nextInline;
			sb.Append(nextInline.Content.ToString());

			nextInline = nextInline.NextSibling as LiteralInline;
		}

		return sb.ToString();
	}

	/// <summary>
	/// 从下一个Inline开始寻找指定类型的Inline
	/// </summary>
	/// <typeparam name="TInline"></typeparam>
	/// <param name="current"></param>
	/// <returns></returns>
	public static TInline? ScanNextInlineOfType<TInline>(Inline? current)
		where TInline : Inline
	{
		if (current == null)
			return null;

		while ((current = current.NextSibling) != null)
		{
			if (current is TInline expected)
			{
				return expected;
			}
		}

			return null;
	}

	/// <summary>
	/// 寻找指定类型的Inline
	/// </summary>
	/// <typeparam name="TInline"></typeparam>
	/// <param name="current"></param>
	/// <returns></returns>
	public static TInline? ScanInlineOfType<TInline>(Inline? current)
		where TInline : Inline
	{
		if (current is TInline target)
			return target;

		return ScanNextInlineOfType<TInline>(current);
	}
}
