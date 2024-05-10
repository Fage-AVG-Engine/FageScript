using Fage.Script.Instruction.Instantization;

namespace Fage.Script.Instruction.Builder;

public class BranchOptionsBuilder
{
	public List<Branch.BranchOption> Options { get; } = new(4);

	public BranchOptionsBuilder Jump(string hintText, string anchorExpression)
	{
		Options.Add(new(hintText, anchorExpression, null));

		return this;
	}

	public BranchOptionsBuilder Jump(string hintText, string location, string? scriptPath)
	{
		if (location.Contains(' '))
			throw new ArgumentException("跳转点id不能包含空格", nameof(location));

		string expression;
		if (scriptPath == null)
			expression = location;
		else
			expression = $"{location} {scriptPath}";

		return Jump(hintText, expression);
	}

	public BranchOptionsBuilder Nop(string hintText)
	{
		Options.Add(new(hintText, null, null));
		return this;
	}

	public BranchOptionsBuilder Code(string hintText, string code)
	{
		Options.Add(new(hintText, null, code));

		return this;
	}
}
