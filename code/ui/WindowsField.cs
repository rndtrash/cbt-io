using Sandbox;
using Sandbox.UI;

namespace CBTIO
{
	[Library("WindowsField")]
	public class WindowsField : Panel
	{
		bool isActive = false;

		public WindowsField()
		{
			AddChild<CodeWindow>( "window cordata" );
		}

		public override void Tick()
		{
			base.Tick();

			if ( Local.Client?.Input.Pressed(InputButton.Duck) ?? false )
			{
				isActive = !isActive;
				SetClass( "open", isActive );
			}
		}
	}
}
