using System.Diagnostics.CodeAnalysis;

namespace Fage.Script.Instruction.Instantization;

public class Code : IInstructionInstantization
{
	public required bool IsBlock { get; set; }
	public required string Content { get; set; }
	public int? LineNumber { get; set; }
	public (int StartPosition, int EndPosition)? Span { get; set; }
}
