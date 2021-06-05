using Sandbox;
using Sandbox.UI;

namespace CBTIO
{
	class Window : Panel
	{
		Vector2 currentPosition = Vector2.Zero;
		Vector2 grabPosition = Vector2.Zero;
		bool followCursor = false;

		public Window()
		{
		}

		public override void Tick()
		{
			if (followCursor)
			{
				UpdatePos( Parent.MousePos - grabPosition );
			}

			//DebugOverlay.ScreenText( $"{Parent.MousePos}/{grabPosition}/{currentPosition}" );
		}

		void UpdatePos()
		{
			UpdatePos( currentPosition );
		}

		void UpdatePos(Vector2 newPos)
		{
			Style.Top = Length.Pixels( newPos.y);
			Style.Left = Length.Pixels( newPos.x);
			Style.Dirty();
		}

		public override void OnButtonEvent( ButtonEvent e )
		{
			//base.OnButtonEvent( e );
			Log.Info( $"Click Event {e.Button} {e.Pressed}" );
			if ( e.Button == "mouseleft" )
			{
				followCursor = e.Pressed;

				if ( followCursor )
				{
					grabPosition = Parent.MousePos - currentPosition;
				}
				else
				{
					currentPosition = Parent.MousePos - grabPosition;
					UpdatePos();
				}
			}
		}
	}
}
