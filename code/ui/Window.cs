using Sandbox;
using Sandbox.UI;
using System;

namespace CBTIO
{
	public class Window : Panel
	{
		public delegate void WindowButtonDelegate();
		public delegate void WindowTitleButtonDelegate( ButtonEvent e );

		public class WindowHeader : Panel
		{
			class WindowButton : Button
			{
				public WindowButtonDelegate OnClick;
				public override void OnButtonEvent( ButtonEvent e )
				{
					base.OnButtonEvent( e );

					if (e.Button == "mouseleft" && !e.Pressed )
						OnClick();
				}
			}

			public Label Title;
			public WindowButtonDelegate OnClose;
			public WindowTitleButtonDelegate OnClick;
			public WindowHeader()
			{
				Title = AddChild<Label>( "title" );
				Title.Text = "Window";
				var b = AddChild<Panel>( "buttons" );
				var close = b.AddChild<WindowButton>( "close" );
				close.Text = "[X]";
				close.OnClick = () => { OnClose(); };
			}

			public override void OnButtonEvent( ButtonEvent e )
			{
				base.OnButtonEvent( e );
				OnClick( e );
			}
		}

		Vector2 currentPosition = Vector2.Zero;
		Vector2 grabPosition = Vector2.Zero;
		bool followCursor = false;

		public WindowHeader Header;
		public Panel Inner;

		public Window()
		{
			Header = AddChild<WindowHeader>( "header" );
			Inner = AddChild<Panel>( "inner" );

			Header.OnClose = () => { Close(); };
			Header.OnClick = ( e ) =>
			{
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
			};
		}

		public override void Tick()
		{
			base.Tick();

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
			Style.Top = Length.Pixels( MathF.Round(newPos.y));
			Style.Left = Length.Pixels( MathF.Round(newPos.x));
			Style.Dirty();
		}

		public virtual void Close()
		{
			Delete( true );
		}
	}
}
