﻿
using Sandbox;
using Sandbox.Hooks;
using Sandbox.UI;
using Sandbox.UI.Construct;
using System;

namespace CBT
{
	public partial class ChatBox : Panel
	{
		static ChatBox Current;

		public Panel Canvas { get; protected set; }
		public TextEntry Input { get; protected set; }

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
			Input.AllowEmojiReplace = true;

			Chat.OnOpenChat += Open;
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

		public void AddEntry( string name, string message, string avatar )
		{
			var e = Canvas.AddChild<ChatEntry>();
			//e.SetFirstSibling();
			e.TimeStamp.Text = $"<{DateTime.Now}>";
			e.Message.Text = message;
			e.NameLabel.Text = name;
			//e.Avatar.SetTexture( avatar );

			e.SetClass( "noname", string.IsNullOrEmpty( name ) );
			e.SetClass( "noavatar", string.IsNullOrEmpty( avatar ) );
		}


		[ClientCmd( "chat_add", CanBeCalledFromServer = true )]
		public static void AddChatEntry( string name, string message, string avatar = null )
		{
			Current?.AddEntry( name, message, avatar );

			// Only log clientside if we're not the listen server host
			if ( !Global.IsListenServer )
			{
				Log.Info( $"{name}: {message}" ); 
			}
		}

		[ClientCmd( "chat_addinfo", CanBeCalledFromServer = true )]
		public static void AddInformation( string message, string avatar = null )
		{
			Current?.AddEntry( null, message, avatar );
		}

		[ServerCmd( "say" )]
		public static void Say( string message )
		{
			Assert.NotNull( ConsoleSystem.Caller );

			// todo - reject more stuff
			if ( message.Contains( '\n' ) || message.Contains( '\r' ) )
				return;

			Log.Info( $"{ConsoleSystem.Caller}: {message}" );
			AddChatEntry( To.Everyone, ConsoleSystem.Caller.Name, message, $"avatar:{ConsoleSystem.Caller.SteamId}" );
		}

	}
}
