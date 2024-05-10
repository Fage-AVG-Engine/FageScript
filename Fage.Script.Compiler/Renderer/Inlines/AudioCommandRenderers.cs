using Fage.Script.Compiler.Syntax.Inlines;
using Fage.Script.Instruction;

namespace Fage.Script.Compiler.Renderer.Inlines;

public class SetBgmRenderer : FageObjectRendererBase<SetBgmCommandInline>
{
	protected override void FageEmit(FageScriptRenderer renderer, InstructionSequenceBuilder builder, SetBgmCommandInline obj)
	{
		builder.SetBgm(obj.Bgm);
	}
}

public class VoiceRenderer : FageObjectRendererBase<VoiceCommandInline>
{
	protected override void FageEmit(FageScriptRenderer renderer, InstructionSequenceBuilder builder, VoiceCommandInline obj)
	{
		builder.StartVoice(obj.Voice);
	}
}

public class SfxRenderer : FageObjectRendererBase<SfxCommandInline>
{
	protected override void FageEmit(FageScriptRenderer renderer, InstructionSequenceBuilder builder, SfxCommandInline obj)
	{
		builder.StartSfx(obj.SoundEffect, obj.Volume);
	}
}
