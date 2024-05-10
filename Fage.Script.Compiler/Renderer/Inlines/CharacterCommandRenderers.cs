using Fage.Script.Compiler.Syntax.Inlines;
using Fage.Script.Instruction;

namespace Fage.Script.Compiler.Renderer.Inlines;

public class AddCharacterRenderer : FageObjectRendererBase<AddCharacterCommandInline>
{
	protected override void FageEmit(FageScriptRenderer renderer, InstructionSequenceBuilder builder, AddCharacterCommandInline obj)
	{
		builder.AddCharacter(obj.CharacterId, obj.Motion, obj.Position);
	}
}

public class RemoveCharacterRenderer : FageObjectRendererBase<RemoveCharacterCommandInline>
{
	protected override void FageEmit(FageScriptRenderer renderer, InstructionSequenceBuilder builder, RemoveCharacterCommandInline obj)
	{
		builder.RemoveCharacter(obj.CharacterId);
	}
}

