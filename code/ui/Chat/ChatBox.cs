
using Sandbox;
using Sandbox.Hooks;
using Sandbox.UI;
using Sandbox.UI.Construct;
using System;
using System.Linq;

namespace CBT
{
	public partial class ChatBox : Panel
	{
		static ChatBox Current;

		public Panel Canvas { get; protected set; }
		public TextEntry Input { get; protected set; }

		public readonly int maxMessages = 10;
		public RealTimeSince TimeSinceLastMessage = 0;

		public ChatBox()
		{
			Current = this;

			//StyleSheet.Load( "/ui/chat/ChatBox.scss" );
			AddClass( "cordata" );

			Canvas = Add.Panel( "chat_canvas" );

			Input = Add.TextEntry( "" );
			Input.AddEvent( "onsubmit", () => Submit() );
			Input.AddEvent( "onblur", () => Close() );
			Input.AcceptsFocus = true;
			Input.AllowEmojiReplace = false;

			Chat.OnOpenChat += Open;
		}

		public override void Tick()
		{
			base.Tick();

			if (TimeSinceLastMessage > 10.0f)
			{
				Canvas.AddClass( "hidden" );
			}
			Log.Info( $"{TimeSinceLastMessage} {Canvas.HasClass("hidden")}" );
		}

		void Open()
		{
			AddClass( "open" );
			Input.Focus();
		}

		void Close()
		{
			RemoveClass( "open" );
			Input.Blur();
		}

		void Submit()
		{
			Close();

			var msg = Input.Text.Trim();
			Input.Text = "";

			if ( string.IsNullOrWhiteSpace( msg ) )
				return;

			Say( msg );
		}

		public void AddEntry( string name, string message )
		{
			TimeSinceLastMessage = 0;
			if (Canvas.HasClass("hidden"))
				Canvas.RemoveClass( "hidden" );

			var e = Canvas.AddChild<ChatEntry>();
			e.TimeStamp.Text = $"<{DateTime.Now.ToShortTimeString()}>";
			e.NameLabel.Text = name;
			if (message != null)
				e.Message.Text = message.Length > 128 ? message[..128] : message;

			e.SetClass( "noname", string.IsNullOrEmpty( name ) );

			var cc = Canvas.ChildCount;
			while (cc > maxMessages)
			{
				Canvas.GetChild( 0 ).Delete(true);
				--cc;
			}
		}


		[ClientCmd( "chat_add", CanBeCalledFromServer = true )]
		public static void AddChatEntry( string name, string message )
		{
			Current?.AddEntry( name, message );

			// Only log clientside if we're not the listen server host
			if ( !Global.IsListenServer )
			{
				Log.Info( $"{name}: {message}" ); 
			}
		}

		[ClientCmd( "chat_addinfo", CanBeCalledFromServer = true )]
		public static void AddInformation( string message )
		{
			Current?.AddEntry( null, message );
		}

		[ServerCmd( "say" )]
		public static void Say( string message )
		{
			Assert.NotNull( ConsoleSystem.Caller );

			// todo - reject more stuff
			if ( message.Contains( '\n' ) || message.Contains( '\r' ) )
				return;

			Log.Info( $"{ConsoleSystem.Caller}: {message}" );
			AddChatEntry( To.Everyone, ConsoleSystem.Caller.Name, message ); // , $"avatar:{ConsoleSystem.Caller.SteamId}"
		}

	}
}
