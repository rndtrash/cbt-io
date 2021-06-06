using Sandbox;
using Sandbox.UI;

namespace CBTIO
{
	class CodeWindow : Window
	{
		TextEntry Code;
		public CodeWindow() : base()
		{
			Header.Title.Text = "Code Window";

			Code = Inner.AddChild<TextEntry>( "code toshibasat" );
			Code.Text = "";
			var p = Inner.AddChild<Panel>( "sidepanel cordata" );
			var l = p.AddChild<Label>( "register" );
			l.Text = "Register A: 0";
			var b = p.AddChild<Button>( "mm big-button" );
			b.Text = "View memory map";
		}

		public override void Close()
		{
			Log.Info( "CodeWindow: am ded" );

			base.Close();
		}
	}
}
