using Sandbox.UI;
using Sandbox.UI.Construct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBTIO
{
	class CodeWindow : Window
	{
		TextEntry Code;
		public CodeWindow() : base()
		{
			Code = AddChild<TextEntry>( "code toshibasat" );
			Code.Text = "";
			var p = AddChild<Panel>( "sidepanel cordata" );
			var l = p.AddChild<Label>( "register" );
			l.Text = "Register A: 0";
			var b = p.AddChild<Button>( "mm" );
			b.Text = "View memory map";
		}
	}
}
