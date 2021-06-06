using Sandbox.UI;

namespace CBTIO
{
	public class WindowsField : Panel
	{
		public WindowsField()
		{
			AddChild<CodeWindow>( "window cordata" );
		}
	}
}
