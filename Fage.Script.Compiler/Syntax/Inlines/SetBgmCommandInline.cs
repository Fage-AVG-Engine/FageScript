using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fage.Script.Compiler.Syntax.Inlines;

public class SetBgmCommandInline : FageCommandInline
{
	public required string Bgm { get; init; }
}
