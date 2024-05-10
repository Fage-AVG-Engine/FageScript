using Fage.Script.Compiler.Parsers;
using Fage.Script.Compiler.Renderer.Blocks;
using Fage.Script.Compiler.Renderer.Inlines;
using Markdig;
using Markdig.Parsers;
using Markdig.Parsers.Inlines;
using Markdig.Renderers;
using System.ComponentModel;

namespace Fage.Script.Compiler;

public class FageScriptExtension : IMarkdownExtension
{
	public static void ConfigurePipeline(MarkdownPipelineBuilder pipeline)
	{
		pipeline.DisableHtml()
			.Use(new FageScriptExtension())
			.UseTaskLists();
	}

	[EditorBrowsable(EditorBrowsableState.Never)]
	public void Setup(MarkdownPipelineBuilder pipeline)
	{
		if (pipeline.Extensions.Count <= 2 && !ReferenceEquals(pipeline.Extensions[0], this))
			throw new InvalidOperationException("FageScriptExtension需要第一个添加到MarkdownPipelineBuilder中");

		pipeline.InlineParsers.TryRemove<HtmlEntityParser>();
		pipeline.InlineParsers.TryRemove<AutolinkInlineParser>();
		pipeline.InlineParsers.TryRemove<EmphasisInlineParser>();
		pipeline.InlineParsers.TryRemove<LinkInlineParser>();

		pipeline.BlockParsers.TryRemove<IndentedCodeBlockParser>();
		pipeline.BlockParsers.TryRemove<ThematicBreakParser>();

		if (!pipeline.BlockParsers.TryFind<ParagraphBlockParser>(out var paraParser))
			throw new InvalidOperationException("不能移除ParagraphBlockParser");

		paraParser.ParseSetexHeadings = false;

		int escapeIndex = pipeline.InlineParsers.FindIndex(p => p is EscapeInlineParser);
		pipeline.InlineParsers.InsertRange(escapeIndex + 1, [
				new Markdig.Extensions.TaskLists.TaskListInlineParser(),
				new SetBgmCommandParser(),
				new BackgroundCommandParser(),
				new AddCharacterCommandParser(),
				new RemoveCharacterCommandParser(),
				new SetBgmCommandParser(),
				new VoiceCommandParser(),
				new SfxCommandParser(),
				new TitleCommandParser(),
				new UnknownCommandParser()
			]);
	}

	[EditorBrowsable(EditorBrowsableState.Never)]
	public void Setup(MarkdownPipeline pipeline, IMarkdownRenderer renderer)
	{
		//if (renderer is not FageScriptRenderer fageRenderer)
		//	throw new ArgumentException("FageScriptExtension必须配合FageScriptRenderer使用");

		var renderers = renderer.ObjectRenderers;
		renderers.AddRange([
			new CodeBlockRenderer(),
			new ParagraphRenderer(),
			new QuoteRenderer(),
			new ListBlockRenderer(),
			new HeadingBlockRenderer(),
			]);

		renderers.AddRange([
			new SetBgmRenderer(),
			new VoiceRenderer(),
			new SfxRenderer(),
			new BackgroundRenderer(),
			new AddCharacterRenderer(),
			new RemoveCharacterRenderer(),
			new CodeInlineRenderer(),
			new JumpRenderer(),
			new ReturnToTitleRenderer(),
			]);
	}
}
