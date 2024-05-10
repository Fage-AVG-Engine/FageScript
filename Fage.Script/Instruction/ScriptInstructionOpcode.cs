using System.Text.Json.Serialization;

namespace Fage.Script.Instruction;

// 添加新的指令 opcode和对应的实例化信息后，
// 需要在IInstructionInstantization添加 JSON 多态标注
// 同时在FageScriptJsonContext添加对应的JsonSerializable

[JsonConverter(typeof(JsonStringEnumConverter<ScriptInstructionOpcode>))]
public enum ScriptInstructionOpcode
{
	// 没什么用，也许可以方便 delta applier
	Nop,
	// 行内 syntax
	Dialogue,
	// 段落起始点
	ParagraphStart,
	// 段落结束点，似乎不是很好用
	// ParagraphEnd,

	// 提交给 neolua，filename参数为{Script.FullName}@{Line}(Span)
	// 以后再区分Block和Inline
	Code,

	SetBgm,
	StartVoice,
	StartSfx,

	AddCharacter,
	RemoveCharacter,
	SetSpeakingCharacter,

	// 锚点，如# Chapter1，作为跳转目标
	Anchor,
	JumpToAnchor,

	SetBackground,

	Branch,

	ReturnToTitle
}