using Sandbox.UI;

//
// You don't need to put things in a namespace, but it doesn't hurt.
//
namespace CBTIO
{
	/// <summary>
	/// This is the HUD entity. It creates a RootPanel clientside, which can be accessed
	/// via RootPanel on this entity, or Local.Hud.
	/// </summary>
	public partial class CBTHudEntity : Sandbox.HudEntity<RootPanel>
	{
		public CBTHudEntity()
		{
			if ( IsClient )
			{
				// RootPanel.SetTemplate( "/minimalhud.html" );
				RootPanel.StyleSheet.Load( "/code/ui/cbt.scss" );

				RootPanel.AddChild<NameTags>( "cordata" );
				RootPanel.AddChild<CBT.ChatBox>();
				RootPanel.AddChild<Scoreboard<ScoreboardEntry>>( "cordata" );
				RootPanel.AddChild<VoiceList>( "cordata" );
			}
		}
	}

}
